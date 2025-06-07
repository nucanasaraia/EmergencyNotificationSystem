using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using EmergencyNotifRespons.SMTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotifRespons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationUserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;

        public AuthenticationUserController(DataContext context, IMapper mapper, IJWTService _jWtService)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = _jWtService;
        }


        [HttpPost("register")]
        public ActionResult Register(AddUser request)
        {
            var userExists = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (userExists != null)
            {
                var response = ApiResponseFactory.BadRequestResponse("User Already Exists");
                return BadRequest(response);
            }

            var user = _mapper.Map<User>(request);
            user.RegistrationDate = DateTime.UtcNow;


            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);


            Random rand = new Random();
            string randomCode = rand.Next(1000, 9999).ToString();

            user.VerificationCode = randomCode;


            SMTPService smtpService = new SMTPService();
            smtpService.SendEmail(user.Email, "Verification", $"<p>{user.VerificationCode}</p>");

            _context.Users.Add(user);
            _context.SaveChanges();


            var responseSuccess = ApiResponseFactory.SuccessResponse(user);
            return Ok(responseSuccess);
        }


        [HttpPost("confirm-email/{email}/{code}")]
        public ActionResult ConfirmEmail(string email, string code)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.VerificationCode == code);

            if (user == null)
            {
                var response = ApiResponseFactory.BadRequestResponse("Something Went Wrong");
                return BadRequest(response);
            }


            user.Status = ACCOUNT_STATUS.VERIFIED;
            user.VerificationCode = null;
            user.IsEmailConfirmed = true;

            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Verified successfully"));
        }



        [HttpPost("login")]
        public ActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                var response = ApiResponseFactory.NotFoundResponse("user not found ");
                return NotFound(response);
            }
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password) && user.Status == ACCOUNT_STATUS.VERIFIED)
                {
                    var response = new ApiResponse<UserToken>
                    {
                        Data = _jwtService.GetUserToken(user),
                        Status = 200,
                        Message = ""
                    };
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseFactory.BadRequestResponse("Something Went Wrong");
                    return BadRequest(response);
                }

            }
        }


        [HttpGet("users-profile")]
        public ActionResult GetProfile(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
               return NotFound(ApiResponseFactory.NotFoundResponse("User Not Found"));
            }
            else
            {
                if (user.Status == ACCOUNT_STATUS.VERIFIED)
                {
                    var response = ApiResponseFactory.SuccessResponse(user);
                    return Ok(response);

                }
                else
                {
                    var response = ApiResponseFactory.BadRequestResponse("user not verified");
                    return BadRequest(response);
                }
            }
        }


        [HttpPut("update-profile/{id}")]
        public ActionResult UpdateProfile(int id, AddUser request)
        {
            var userExist = _context.Users.FirstOrDefault(x => x.Id == id);

            if (userExist == null)
            {
                var badresponse = ApiResponseFactory.NotFoundResponse("user not found");
                return NotFound(badresponse);
            }

            if (userExist.Status != ACCOUNT_STATUS.VERIFIED)
            {
                return BadRequest(ApiResponseFactory.BadRequestResponse("User Not Verified"));
            }

            userExist.UserName = request.UserName;
            userExist.FirstName = request.FirstName;
            userExist.LastName = request.LastName;
            userExist.PhoneNumber = request.PhoneNumber;
            userExist.Location = request.Location;

            _context.SaveChanges();

            var responseSuccess = ApiResponseFactory.SuccessResponse("Profile updated successfully");
            return Ok(responseSuccess);
        }



        [HttpDelete("delete-user")]
        public ActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                var notfoundresp = ApiResponseFactory.NotFoundResponse("user not found");
                return NotFound(notfoundresp);
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            var resp = ApiResponseFactory.SuccessResponse("User Removed Succesfully");
            return Ok();
        }
    }
}

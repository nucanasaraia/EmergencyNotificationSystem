using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmergencyNotifRespons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IJWTService _jwtservice;
        private readonly IMapper _mapper;


        public VolunteerController(DataContext context, IJWTService jWTService, IMapper mapper)
        {
            _context = context;
            _jwtservice = jWTService;
            _mapper = mapper;
        }

        [HttpPost("register-as-volunteer")]
        public ActionResult RegisterVolunteer(AddVolunteer request)
        {
            var volunteer = _context.Volunteers.FirstOrDefault(v => v.Id == request.UserId);
            if (volunteer == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("with this id Volunteer already exists"));
            }

            var vol = _mapper.Map<Volunteer>(request);

            _context.Volunteers.Add(vol);
            _context.SaveChanges();

            var response = ApiResponseFactory.SuccessResponse("Succesfully Joined As Volunteer");
            return Ok(response);
        }


        [HttpGet("get-volunteers")]
        public ActionResult GetVolunteerDetails()
        {
            var volunteers = _context.Volunteers
                   .Include(v => v.User)
                   .Select(v => v.User)
                   .ToList();

            if (!volunteers.Any())
            {
                var response = ApiResponseFactory.NotFoundResponse("Not Found");
                return BadRequest(response);
            }

            var result = _mapper.Map<List<VolunteersDetailDto>>(volunteers);
            return Ok(result);
        }


        [HttpGet("get-specific-volunteer/{id}")]
        public ActionResult GetSpecVol(int id)
        {
            var volunteer = _context.Volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer == null)
            {
                return BadRequest(ApiResponseFactory.NotFoundResponse("Volunteer Not Found"));
            }

            var result = _mapper.Map<List<VolunteersDetailDto>>(volunteer);
            return Ok(result);
        }


        [HttpPut("update-volunteer/{id}")]
        public ActionResult UpdateVolunteer(int id, UpdateVolunteerDto request)
        {
            var volunteer = _context.Volunteers.FirstOrDefault(v => v.Id == id);
            if (volunteer == null)
            {
                return BadRequest(ApiResponseFactory.NotFoundResponse("Volunteer Not Found"));
            }

            _mapper.Map(volunteer, request);
            _context.SaveChanges();

            var response = ApiResponseFactory.SuccessResponse("Volunteer Updated Succesfully");
            return Ok(response);
        }


        [HttpPut("update-volunteer-status/{id}")]
        public ActionResult UpdateStatus(int id, AVAILABILITY_STATUS status)
        {
            var vol = _context.Volunteers.FirstOrDefault(v => v.Id == id);
            if (vol == null)
            {
                return BadRequest(ApiResponseFactory.NotFoundResponse("Volunteer Not Found"));
            }

            vol.AvailabilityStatus = status;
            _context.SaveChanges();

            var response = ApiResponseFactory.SuccessResponse("Successfully Changed Status");
            return Ok(response);
        }

        [HttpDelete("delete-volunteer/{id}")]
        public ActionResult DeleteStatus(int id) 
        {
            var vol = _context.Volunteers.FirstOrDefault(v => v.Id == id);
            if (vol == null)
            {
                return BadRequest(ApiResponseFactory.NotFoundResponse("Volunteer Not Found"));
            }
            _context.Volunteers.Remove(vol);
            _context.SaveChanges();

            var response = ApiResponseFactory.SuccessResponse("Deleted Successfully");
            return Ok(response);
        }

    }
}

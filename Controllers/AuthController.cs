using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Implementation;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EmergencyNotifRespons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly DataContext _context;
        public AuthController(IAuthService authService, ITokenService tokenService, DataContext context)
        {
            _authService = authService;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Registration(request);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
        {
            var result = await _authService.VerifyEmail(request);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LogInRequest request)
        {
            var response = await _authService.LogIn(request);

            if (response.Status != HttpStatusCode.OK || response.Data == null)
                return StatusCode((int)response.Status, response);

            Response.Cookies.Append("refreshToken", response.Data.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("No refresh token found");

            var response = await _authService.RefreshToken(refreshToken);

            if (response.Status != HttpStatusCode.OK || response.Data == null)
                return StatusCode((int)response.Status, response);

            Response.Cookies.Append("refreshToken", response.Data.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(response);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var result = await _authService.ForgotPassword(request.Email);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(PasswordResetRequest request)
        {
            var result = await _authService.ResetPassword(request);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
                await _authService.Logout(refreshToken);

            Response.Cookies.Delete("refreshToken");
            return Ok("Logged out");
        }
    }
}

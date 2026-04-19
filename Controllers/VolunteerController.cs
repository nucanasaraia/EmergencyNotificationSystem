using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmergencyNotifRespons.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerService _volunteerService;
        public VolunteerController(IVolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsVolunteer([FromBody] AddVolunteer request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _volunteerService.RegisterAsVolunteer(userId, request);
            return StatusCode((int)result.Status, result);
        }

        [HttpPut("{volunteerId}/availability")]
        public async Task<IActionResult> UpdateAvailability(int volunteerId, AVAILABILITY_STATUS status)
        {
            var result = await _volunteerService.UpdateAvailability(volunteerId, status);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("{volunteerId}/assign")]
        public async Task<IActionResult> AssignToEvent(int volunteerId, [FromQuery] int eventId)
        {
            var assignedById = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _volunteerService.AssignToEvent(volunteerId, eventId, assignedById);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("{volunteerId}/complete")]
        public async Task<IActionResult> CompleteAssignment(int volunteerId, int assignmentId)
        {
            var result = await _volunteerService.CompleteAssignment(volunteerId, assignmentId);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetVolunteerProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _volunteerService.GetVolunteerProfile(userId);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("availability/{status}")]
        public async Task<IActionResult> GetVolunteersByAvailability(AVAILABILITY_STATUS status)
        {
            var result = await _volunteerService.GetVolunteersByAvailability(status);
            return StatusCode((int)result.Status, result);
        }
    }
}

using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotifRespons.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyEventController : ControllerBase
    {
        private readonly IEmergencyEventService _emergencyEventService;
        public EmergencyEventController(IEmergencyEventService emergencyEventService)
        {
            _emergencyEventService = emergencyEventService;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateEvent([FromBody] AddEmergencyEvent request)
        {
            var result = await _emergencyEventService.CreateEvent(request);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var result = await _emergencyEventService.GetEventById(id);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetEmergencyEvents([FromQuery] EVENT_TYPE? type = null)
        {
            var result = await _emergencyEventService.GetEmergencyEvents(type);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveEvents([FromQuery] ACTIVITY_STATUS status = ACTIVITY_STATUS.ACTIVE)
        {
            var result = await _emergencyEventService.GetActiveEvents(status);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyEvents([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] decimal affectedRadius)
        {
            var result = await _emergencyEventService.GetNearbyEvents(latitude, longitude, affectedRadius);
            return StatusCode((int)result.Status, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] AddEmergencyEvent request)
        {
            var result = await _emergencyEventService.UpdateEvent(id, request);
            return StatusCode((int)result.Status, result);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateEventStatus(int id, [FromQuery] ACTIVITY_STATUS status)
        {
            var result = await _emergencyEventService.UpdateEventStatus(id, status);
            return StatusCode((int)result.Status, result);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _emergencyEventService.DeleteEvent(id);
            return StatusCode((int)result.Status, result);
        }
    }
}

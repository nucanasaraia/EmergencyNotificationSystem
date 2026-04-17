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
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesService _resourcesService;
        public ResourcesController(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateResource([FromBody] AddResource request)
        {
            var result = await _resourcesService.CreateResource(request);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceById(int id)
        {
            var result = await _resourcesService.GetResourceById(id);
            return StatusCode((int)result.Status, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(int id, AddResource request)
        {
            var result = await _resourcesService.UpdateResource(id, request);
            return StatusCode((int)result.Status, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var result = await _resourcesService.DeleteResource(id);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("{resourceId}/assign")]  
        public async Task<IActionResult> AssignToEvent(int resourceId, [FromQuery] int eventId, [FromQuery] int assignedById)
        {
            var result = await _resourcesService.AssignToEvent(resourceId, eventId, assignedById);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("return/{assignmentId}")]
        public async Task<IActionResult> ReturnResource(int assignmentId)
        {
            var result = await _resourcesService.ReturnResource(assignmentId);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableResources(RESOURCE_CATEGORY? category = null)
        {
            var result = await _resourcesService.GetAvailableResources(category);
            return StatusCode((int)result.Status, result);
        }
    }
}

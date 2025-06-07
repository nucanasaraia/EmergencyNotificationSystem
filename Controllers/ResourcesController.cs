using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotifRespons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IJWTService _jwtservice;
        private readonly IMapper _mapper;


        public ResourcesController(DataContext context, IJWTService jWTService, IMapper mapper)
        {
            _context = context;
            _jwtservice = jWTService;
            _mapper = mapper;
        }

        [HttpPost("add-resources")]
        public ActionResult AddResource(AddResource request)
        {

            var resources = _mapper.Map<Resource>(request);
            _context.Resources.Add(resources);
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Resource Added Successfully"));
        }

        [HttpGet("get-resources")]
        public ActionResult GetResources(RESOURCE_CATEGORY category)
        {
            var resources = _context.Resources.Where(r => r.Category == category).ToList();

            if (resources == null)
            {
                return BadRequest();
            }

            var resor = _mapper.Map<List<Resource>>(resources);
            return Ok(resor);
        }

        [HttpPut("change-resource/{id}")]
        public ActionResult ChangeResource(int id, AddResource request)
        {
            var resource = _context.Resources.FirstOrDefault(r => r.Id == id);
            if (resource == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("Resource Not Found"));
            }

            _mapper.Map(resource, request);
            _context.SaveChanges();
            return Ok(ApiResponseFactory.SuccessResponse("Resource Changed Successfully"));
        }

        [HttpPost("add-resources/assignments")]
        public ActionResult AddAssigment(AddResourceAssignment request)
        {
            var resource = _context.Resources.Any(r => r.Id == request.ResourceId);
            var events = _context.EmergencyEvents.Any(e => e.Id == request.EmergencyEventId);

            if (!resource || !events)
                return NotFound(ApiResponseFactory.NotFoundResponse("Resource or Emergency Event not found."));

            var assignment = _mapper.Map<ResourceAssignment>(request);

            assignment.AssignedById = _jwtservice.GetCurrentUserId();


            if (assignment.AssignedTime == default)
                assignment.AssignedTime = DateTime.UtcNow;

            _context.ResourceAssignments.Add(assignment);
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Rsource Assignment Added Successfully"));
        }


        [HttpPut("resources/assignments/{id}/return")]
        public ActionResult ReturnAssignment(int id)
        {
            var resourceAssignment = _context.ResourceAssignments.FirstOrDefault(r => r.Id == id);
            if (resourceAssignment == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("Resource Assignment not found."));
            }

            resourceAssignment.Status = STATUS2.RETURNED;
            resourceAssignment.ReturnedTime = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Resource Assignment marked as returned successfully."));
        }


    }
}

using AutoMapper;
using Azure.Core;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmergencyNotifRespons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerAssignmentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IJWTService _jwtservice;
        private readonly IMapper _mapper;
        public VolunteerAssignmentsController(DataContext context, IJWTService jWTService, IMapper mapper)
        {
            _context = context;
            _jwtservice = jWTService;
            _mapper = mapper;
        }


        [HttpPost("add-volunteer-assignments")]
        public ActionResult AddVolAssg(AddVolunteerAssignment request)
        {
            var volunteer = _context.Volunteers.Any(v => v.Id == request.VolunteerId);
            var emergency = _context.EmergencyEvents.Any(e => e.Id == request.EmergencyEventId);
            
            if (!volunteer || !emergency)
                return NotFound(ApiResponseFactory.NotFoundResponse("Volunteer or Emergency Event not found."));

            var assignment = _mapper.Map<VolunteerAssignment>(request);

            assignment.AssignedById = _jwtservice.GetCurrentUserId();


            if (assignment.AssignedTime == default)
                assignment.AssignedTime = DateTime.UtcNow;

            _context.VolunteerAssignments.Add(assignment);
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Rsource Assignment Added Successfully"));
        }

        [HttpGet("volunteer-assignments/my")]
        public ActionResult GetMyVolunteerAssignments()
        {
            int currentUserId = _jwtservice.GetCurrentUserId(); 

            var volunteer = _context.Volunteers.FirstOrDefault(v => v.UserId == currentUserId);
            if (volunteer == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("Volunteer not found."));
            }

            var myAssignments = _context.VolunteerAssignments
                .Where(va => va.VolunteerId == volunteer.Id)
                .ToList();

            return Ok(ApiResponseFactory.SuccessResponse(myAssignments));
        }


        [HttpPut("volunteer-assignments/{id}/status")]
        public ActionResult ChangeStatus(int id, STATUS3 status)
        {
            var assignment = _context.VolunteerAssignments.FirstOrDefault(v => v.Id == id);
            if(assignment == null)
            {
                return NotFound();
            }

            assignment.Status = status;
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Status Changed Successfully"));
        }
    }
}

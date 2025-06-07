using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Helpers;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotifRespons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyEventController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IJWTService _jwtservice;
        private readonly IMapper _mapper;


        public EmergencyEventController(DataContext context, IJWTService jWTService, IMapper mapper)
        {
            _context = context;
            _jwtservice = jWTService;
            _mapper = mapper;
        }
        [HttpPost("add-emergencyEvents")]
        public ActionResult AddEvent(AddEmergencyEvent request)
        {
            try
            {
                var user = _context.Users.Find(request.CreatedById);
                if (user == null)
                {
                    return BadRequest("Invalid CreatedById. User not found.");
                }

                var emergency = _mapper.Map<EmergencyEvent>(request);
                _context.EmergencyEvents.Add(emergency);
                _context.SaveChanges();

                return Ok(ApiResponseFactory.SuccessResponse("Emergency Event Added Successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpGet("get-emergencies")]

        public ActionResult GetEmergencies(EVENT_TYPE type)
        {
            var emergency = _context.EmergencyEvents.Where(e => e.EVENT_TYPE == type).ToList();

            if (emergency == null)
            {
                return BadRequest();
            }
            else
            {

                var result = _mapper.Map<List<EmergencyEvent>>(emergency);

                return Ok(result);
            }
        }

        [HttpGet("get-emergency/{id}")]
        public ActionResult GetSpecific(int id)
        {
            var emgExists = _context.EmergencyEvents.FirstOrDefault(e => e.Id == id);
            if (emgExists == null)
            {
                var badResponse = ApiResponseFactory.NotFoundResponse("Event not found");
                return NotFound(badResponse);
            }

            var result = _mapper.Map<AddEmergencyEvent>(emgExists);
            return Ok(result);
        }


        [HttpPost("update-emergencyEvents/{id}")]
        public ActionResult UpdateEvent(int id, AddEmergencyEvent request)
        {
            var emergencyEvent = _context.EmergencyEvents.FirstOrDefault(e => e.Id == id);
            if (emergencyEvent == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("Event not found"));
            }

            _mapper.Map(emergencyEvent, request);
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Emergency Event Updated Successfully"));
        }


        [HttpPut("update-emergencies/{id}/status")]
        public ActionResult UpdateStatus(int id, ACTIVITY_STATUS status)
        {
            var emergency = _context.EmergencyEvents.FirstOrDefault(e => e.Id == id);
            if (emergency == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("Event not found"));
            }

            emergency.ACTIVITY_STATUS = status;
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Status Changed Successfully"));
        }


        //GET /api/emergencies/nearby?latitude=41.7151&longitude=44.8271
        [HttpGet("emergencyEvents-nearby")]
        public ActionResult GetNearbyEmergencies([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radiusKm = 5)
        {
            var nearbyEvents = _context.EmergencyEvents
                .Where(e => e.ACTIVITY_STATUS == ACTIVITY_STATUS.ACTIVE)
                .ToList()
                .Where(e => GeoHelper.GetDistance(latitude, longitude, e.Latitude, e.Longitude) <= radiusKm)
                .ToList();

            var response = ApiResponseFactory.SuccessResponse(nearbyEvents);
            return Ok(response);
        }

        [HttpDelete("delete-emergencyEvent")]
        public ActionResult DeleteEvent(int id)
        {
            var events = _context.Users.FirstOrDefault(u => u.Id == id);

            if (events == null)
            {
                var notfoundresp = ApiResponseFactory.NotFoundResponse("Event not found");
                return NotFound(notfoundresp);
            }

            _context.Users.Remove(events);
            _context.SaveChanges();

            var resp = ApiResponseFactory.SuccessResponse("Enevt Removed Succesfully");
            return Ok();
        }

    }
}

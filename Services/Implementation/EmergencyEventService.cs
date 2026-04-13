using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using System.Net;

namespace EmergencyNotifRespons.Services.Implementation
{
    public class EmergencyEventService : IEmergencyEventService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EmergencyEventService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EmergencyEventDto>> CreateEvent(AddEmergencyEvent request)
        {
            try
            {
                //_currentuseer = request.CreatedById;
                var emergencyEvent = _mapper.Map<EmergencyEventService>(request);
                await _context.AddAsync(emergencyEvent);
                await _context.SaveChangesAsync();

                var result = _mapper.Map<EmergencyEventDto>(emergencyEvent);
                return ApiResponseFactory.SuccessResponse(result, HttpStatusCode.OK);
            }
            catch
            {
                return ApiResponseFactory.ErrorResponse<EmergencyEventDto>(HttpStatusCode.BadRequest, "something went wrong");
            }
        }

        public async Task<ApiResponse<string>> DeleteEvent(int id)
        {
            var emergencyEvent = await _context.EmergencyEvents.FindAsync(id);
            if (emergencyEvent == null)
            {
                
            }

            _context.EmergencyEvents.Remove(emergencyEvent);
            await _context.SaveChangesAsync();
        }

        public Task<ApiResponse<List<EmergencyEventDto>>> GetEmergencyEvents(EVENT_TYPE type)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<EmergencyEventDto>>> GetNearbyEmergencies(double latitude, double longitude, double radiusKm)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<EmergencyEventDto>> GetSpecificEvent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> UpdateEvent(int id, AddEmergencyEvent request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> UpdateEventStatus(int id, ACTIVITY_STATUS status)
        {
            throw new NotImplementedException();
        }
    }
}

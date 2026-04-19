using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Helpers;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ApiResponse<EmergencyEventDto>> CreateEvent(AddEmergencyEvent request, int createdById)
        {
            try
            {
                var emergencyEvent = _mapper.Map<EmergencyEvent>(request);
                emergencyEvent.CreatedById = createdById;
                await _context.EmergencyEvents.AddAsync(emergencyEvent);
                await _context.SaveChangesAsync();

                // reload with CreatedBy included
                await _context.Entry(emergencyEvent)
                    .Reference(e => e.CreatedBy)
                    .LoadAsync();

                var response = _mapper.Map<EmergencyEventDto>(emergencyEvent);
                return ApiResponseFactory.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<EmergencyEventDto>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> DeleteEvent(int id)
        {
            try
            {
                var existingEvent = await _context.EmergencyEvents.FirstOrDefaultAsync(e => e.Id == id);
                if (existingEvent == null)
                {
                    return ApiResponseFactory.NotFound<string>();
                }

                _context.EmergencyEvents.Remove(existingEvent);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("Event deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<EmergencyEventDto>>> GetActiveEvents(ACTIVITY_STATUS status = ACTIVITY_STATUS.ACTIVE)
        {
            try
            {
                var events = await _context.EmergencyEvents
                    .Include(e => e.CreatedBy)   
                    .Where(e => e.ACTIVITY_STATUS == status)
                    .ToListAsync();

                var eventDtos = _mapper.Map<List<EmergencyEventDto>>(events);
                return ApiResponseFactory.Success(eventDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<EmergencyEventDto>>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<EmergencyEventDto>>> GetEmergencyEvents(EVENT_TYPE? type = null)
        {
            try
            {
                var events = await _context.EmergencyEvents
                    .Include(e => e.CreatedBy)   
                    .Where(e => type == null || e.EVENT_TYPE == type)
                    .ToListAsync();

                var eventDtos = _mapper.Map<List<EmergencyEventDto>>(events);
                return ApiResponseFactory.Success(eventDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<EmergencyEventDto>>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<EmergencyEventDto>> GetEventById(int id)
        {
            try
            {
                var emergencyEvent = await _context.EmergencyEvents
                    .Include(e => e.CreatedBy)   
                    .FirstOrDefaultAsync(e => e.Id == id);
                
                if (emergencyEvent == null)
                {
                    return ApiResponseFactory.NotFound<EmergencyEventDto>();
                }

                var response = _mapper.Map<EmergencyEventDto>(emergencyEvent);
                return ApiResponseFactory.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<EmergencyEventDto>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<EmergencyEventDto>>> GetNearbyEvents(double latitude, double longitude, decimal affectedRadius)
        {
            try
            {
                var allEvents = await _context.EmergencyEvents
                    .Include(e => e.CreatedBy)   
                    .ToListAsync();

                var nearbyEvents = allEvents
                    .Where(e => GeoHelper.GetDistance(latitude, longitude, e.Latitude, e.Longitude)
                                <= (double)affectedRadius)
                    .ToList();
                var eventDtos = _mapper.Map<List<EmergencyEventDto>>(nearbyEvents);
                return ApiResponseFactory.Success(eventDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<EmergencyEventDto>>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> UpdateEvent(int id, AddEmergencyEvent request)
        {
            try
            {
                var existingEvent = await _context.EmergencyEvents.FirstOrDefaultAsync(e => e.Id == id);
                if (existingEvent == null)
                {
                    return ApiResponseFactory.NotFound<string>();
                }

                _mapper.Map(request, existingEvent);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("Event updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> UpdateEventStatus(int id, ACTIVITY_STATUS status)
        {
            try
            {
                var existingEvent = await _context.EmergencyEvents.FirstOrDefaultAsync(e => e.Id == id);
                if (existingEvent == null)
                {
                    return ApiResponseFactory.NotFound<string>();
                }

                existingEvent.ACTIVITY_STATUS = status;
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("Event status updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IEmergencyEventService
    {
        Task<ApiResponse<EmergencyEventDto>> GetEventById(int id);
        Task<ApiResponse<EmergencyEventDto>> CreateEvent(AddEmergencyEvent request, int createdById);
        Task<ApiResponse<List<EmergencyEventDto>>> GetEmergencyEvents(EVENT_TYPE? type = null);
        Task<ApiResponse<List<EmergencyEventDto>>> GetActiveEvents(ACTIVITY_STATUS status = ACTIVITY_STATUS.ACTIVE);
        Task<ApiResponse<List<EmergencyEventDto>>> GetNearbyEvents(double latitude, double longitude, decimal affectedRadius);
        Task<ApiResponse<string>> UpdateEvent(int id, AddEmergencyEvent request);
        Task<ApiResponse<string>> UpdateEventStatus(int id, ACTIVITY_STATUS status);
        Task<ApiResponse<string>> DeleteEvent(int id);
    }
}
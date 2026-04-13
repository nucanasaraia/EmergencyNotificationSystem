using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IEmergencyEventService
    {
        Task<ApiResponse<EmergencyEventDto>> CreateEvent(AddEmergencyEvent request);
        Task<ApiResponse<List<EmergencyEventDto>>> GetEmergencyEvents(EVENT_TYPE type);
        Task<ApiResponse<EmergencyEventDto>> GetSpecificEvent(int id);
        Task<ApiResponse<List<EmergencyEventDto>>> GetNearbyEmergencies(double latitude, double longitude, double radiusKm);
        Task<ApiResponse<string>> UpdateEvent(int id, AddEmergencyEvent request);
        Task<ApiResponse<string>> UpdateEventStatus(int id, ACTIVITY_STATUS status);
        Task<ApiResponse<string>> DeleteEvent(int id);
    }
}

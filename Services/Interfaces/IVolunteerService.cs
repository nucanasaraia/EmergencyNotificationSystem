using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Requests;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IVolunteerService
    {
        Task<ApiResponse<string>> RegisterAsVolunteer(int userId, AddVolunteer request);
        Task<ApiResponse<string>> UpdateAvailability(int volunteerId, AVAILABILITY_STATUS status);
        Task<ApiResponse<string>> AssignToEvent(int volunteerId, int eventId, int assignedById);
        Task<ApiResponse<string>> CompleteAssignment(int volunteerId, int assignmentId);
        Task<ApiResponse<VolunteerDto>> GetVolunteerProfile(int userId);
        Task<ApiResponse<List<VolunteerDto>>> GetVolunteersByAvailability(AVAILABILITY_STATUS status);
    }
}
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IResourcesService
    {
        Task<ApiResponse<ResourceDto>> GetResourceById(int id);
        Task<ApiResponse<ResourceDto>> CreateResource(AddResource request);
        Task<ApiResponse<string>> UpdateResource(int id, AddResource request);
        Task<ApiResponse<string>> DeleteResource(int id);
        Task<ApiResponse<string>> AssignToEvent(int resourceId, int eventId, int assignedById);
        Task<ApiResponse<string>> ReturnResource(int assignmentId);
        Task<ApiResponse<List<ResourceAssignmentDto>>> GetResourceAssignments(int resourceId);
        Task<ApiResponse<List<ResourceDto>>> GetAvailableResources(RESOURCE_CATEGORY? category = null);
    }
}
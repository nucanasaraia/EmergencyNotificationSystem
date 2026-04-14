using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UserDto>> GetUserById(int id);
        Task<ApiResponse<string>> UpdateUser(int id, AddUser request);
        Task<ApiResponse<List<UserDto>>> GetAllUsers(ROLES_TYPE? role = null);
        Task<ApiResponse<string>> ChangeUserRole(int id, ROLES_TYPE type);
    }
}
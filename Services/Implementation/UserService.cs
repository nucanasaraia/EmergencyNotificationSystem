using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;

namespace EmergencyNotifRespons.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<ApiResponse<string>> ChangeUserRole(int id, ROLES_TYPE type)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<UserDto>>> GetAllUsers(ROLES_TYPE? role = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserDto>> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> UpdateUser(int id, AddUser request)
        {
            throw new NotImplementedException();
        }
    }
}

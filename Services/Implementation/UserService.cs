using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        public async Task<ApiResponse<string>> ChangeUserRole(int id, ROLES_TYPE type)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return ApiResponseFactory.NotFound<string>("User not found");
                }

                user.Role = type;
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("User role updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while updating user role: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<UserDto>>> GetAllUsers(ROLES_TYPE? role = null)
        {
            try
            {
                var users = await _context.Users
                    .Where(u => role == null || u.Role == role)
                    .ToListAsync();

                var userDtos = _mapper.Map<List<UserDto>>(users);
                return ApiResponseFactory.Success(userDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<UserDto>>("An error occurred while retrieving users: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return ApiResponseFactory.NotFound<UserDto>("User not found");
                }

                var response = _mapper.Map<UserDto>(user);
                return ApiResponseFactory.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<UserDto>("An error occurred while retrieving user: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> UpdateUser(int id, UpdateUser request)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return ApiResponseFactory.NotFound<string>("User not found");
                }

                _mapper.Map(request, user);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("User updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>("An error occurred while updating user: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}

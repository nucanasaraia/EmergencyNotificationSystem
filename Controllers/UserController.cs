using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotifRespons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
           var result = await _userService.GetUserById(id);
           return StatusCode((int)result.Status, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, AddUser request)
        {
            var result = await _userService.UpdateUser(id, request);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(ROLES_TYPE? role = null)
        {
            var result = await _userService.GetAllUsers(role);
            return StatusCode((int)result.Status, result);
        }

        [HttpPut("{id}/change-role")]
        public async Task<IActionResult> ChangeUserRole(int id, ROLES_TYPE type)
        {
            var result = await _userService.ChangeUserRole(id, type);
            return StatusCode((int)result.Status, result);
        }
    }
}

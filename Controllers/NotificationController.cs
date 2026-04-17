using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmergencyNotifRespons.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("send/{eventId}")]
        public async Task<IActionResult> SendNotification(int eventId, [FromBody] AddNotifications request)
        {
            var result = await _notificationService.SendNotification(eventId, request);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {   
            var result = await _notificationService.MarkAsRead(notificationId);
            return StatusCode((int)result.Status, result);
        }

        [HttpPost("mark-all-as-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _notificationService.MarkAllAsRead(userId);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("user-notifications")]
        public async Task<IActionResult> GetUserNotifications()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _notificationService.GetUserNotifications(userId);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("notifications-by-type/{type}")]
        public async Task<IActionResult> GetNotificationsByType(NOTIFICATION_TYPE type)
        {
            var result = await _notificationService.GetNotificationsByType(type);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _notificationService.GetUnreadCount(userId);
            return StatusCode((int)result.Status, result);
        }
    }
}

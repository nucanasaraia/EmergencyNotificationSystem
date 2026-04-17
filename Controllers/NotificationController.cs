using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotifRespons.Controllers
{
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

        [HttpPost("mark-all-as-read/{userId}")]
        public async Task<IActionResult> MarkAllAsRead(int userId)
        {
            var result = await _notificationService.MarkAllAsRead(userId);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("user-notifications/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var result = await _notificationService.GetUserNotifications(userId);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("notifications-by-type/{type}")]
        public async Task<IActionResult> GetNotificationsByType(NOTIFICATION_TYPE type)
        {
            var result = await _notificationService.GetNotificationsByType(type);
            return StatusCode((int)result.Status, result);
        }

        [HttpGet("unread-count/{userId}")]
        public async Task<IActionResult> GetUnreadCount(int userId)
        {
            var result = await _notificationService.GetUnreadCount(userId);
            return StatusCode((int)result.Status, result);
        }
    }
}

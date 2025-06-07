using AutoMapper;
using Azure.Core;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using EmergencyNotifRespons.SMTP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyNotifRespons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyNotificationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IJWTService _jwtservice;
        private readonly IMapper _mapper;


        public EmergencyNotificationController(DataContext context, IJWTService jWTService, IMapper mapper)
        {
            _context = context;
            _jwtservice = jWTService;
            _mapper = mapper;
        }


        [HttpPost("add-notifications")]
        public ActionResult AddNotif(AddNotifications request)
        {
            var notife = _mapper.Map<EmergencyNotification>(request);
            _context.EmergencyNotifications.Add(notife);
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Notification added Successfully"));
        }

        [HttpGet("emergency-noticfications")]
        public ActionResult GetNotifications()
        {
            var notifies = _context.EmergencyNotifications.ToList();

            var result = _mapper.Map<List<AddNotifications>>(notifies);

            return Ok(ApiResponseFactory.SuccessResponse(result));
        }


        [HttpPost("broadcast")]
        public ActionResult BroadcastNotification([FromBody] BroadcastNotificationDto dto)
        {
            var emergencyEvent = _context.EmergencyEvents.FirstOrDefault(e => e.Id == dto.EmergencyEventId);
            if (emergencyEvent == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("Emergency event not found."));
            }

            var notif = new EmergencyNotification
            {
                EmergencyEventId = dto.EmergencyEventId,
                Message = dto.Message,
                SentTime = DateTime.UtcNow,
                NotificationType = dto.NotificationType
            };

            _context.EmergencyNotifications.Add(notif);
            _context.SaveChanges(); 

            var users = _context.Users
                   .Where(u => u.Role == ROLES_TYPE.VOLUNTEER &&
                               u.Status == ACCOUNT_STATUS.VERIFIED &&
                               u.IsEmailConfirmed)
                   .ToList();

            var userNotifications = new List<UserNotification>();

            foreach (var user in users)
            {
                userNotifications.Add(new UserNotification
                {
                    UserId = user.Id,
                    NotificationId = notif.Id,
                    IsRead = false,
                    DeliveryStatus = DELIVERY_STATUS.SENT
                });

                SMTPService smtpService = new SMTPService();
                smtpService.SendEmail(user.Email, "🚨 Emergency Notification", dto.Message);
            }

            _context.UserNotifications.AddRange(userNotifications);
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Notification broadcasted and emails sent."));
        }


        [HttpPost("users-notifications")]
        public ActionResult AddNotif(AddUSersNotifications request)
        {
            var userExists = _context.Users.Any(u => u.Id == request.UserId);
            var notifExists = _context.EmergencyNotifications.Any(n => n.Id == request.NotificationId);

            if (!userExists || !notifExists)
                return NotFound(ApiResponseFactory.NotFoundResponse("User or notification not found."));

            var userNotification = _mapper.Map<UserNotification>(request);
    
            _context.UserNotifications.Add(userNotification);
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("User notification added."));
        }


        [HttpGet("get-user-notifications")]
        public ActionResult GetUserNotifications([FromQuery] int userId, [FromQuery] int? notificationId = null)
        {
            var userExists = _context.Users.Any(u => u.Id == userId);
            if (!userExists)
                return NotFound(ApiResponseFactory.NotFoundResponse("User not found."));

            var query = _context.UserNotifications
                                .Where(un => un.UserId == userId);

            if (notificationId.HasValue)
            {
                query = query.Where(un => un.NotificationId == notificationId.Value);
            }

            var userNotifications = query.ToList();

            return Ok(ApiResponseFactory.SuccessResponse(userNotifications));
        }


        [HttpPut("notifications/{id}/read")]
        public ActionResult MarkNotificationAsRead(int id)
        {
            var userNotification = _context.UserNotifications.FirstOrDefault(un => un.Id == id);
            if (userNotification == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("Notification not found for this user."));
            }

            userNotification.IsRead = true;
            _context.SaveChanges();

            var result = _mapper.Map<AddUSersNotifications>(userNotification);
            return Ok(ApiResponseFactory.SuccessResponse(result));
        }


        [HttpDelete("delete-notification")]
        public ActionResult DeleteNotif([FromQuery] int userId, [FromQuery] int notificationId)
        {
            var userNotification = _context.UserNotifications
                .FirstOrDefault(un => un.UserId == userId && un.NotificationId == notificationId);

            if (userNotification == null)
            {
                return NotFound(ApiResponseFactory.NotFoundResponse("User notification not found."));
            }

            _context.UserNotifications.Remove(userNotification);
            _context.SaveChanges();

            return Ok(ApiResponseFactory.SuccessResponse("Notification deleted successfully."));
        }

    }
}

using AutoMapper;
using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Data;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Models;
using EmergencyNotifRespons.Requests;
using EmergencyNotifRespons.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EmergencyNotifRespons.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public NotificationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<NotificationDto>>> GetNotificationsByType(NOTIFICATION_TYPE type)
        {
            try
            {
                var notifications = await _context.EmergencyNotifications
                .Where(n => n.NotificationType == type)
                .ToListAsync();

                var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);
                return ApiResponseFactory.Success(notificationDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<NotificationDto>>($"Error retrieving notifications: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<int>> GetUnreadCount(int userId)
        {
            try
            {
                var notifications = await _context.EmergencyNotifications
                .Include(n => n.UserNotifications)
                .FirstOrDefaultAsync(n => n.UserNotifications.Any(un => un.UserId == userId && !un.IsRead));

                if (notifications == null)
                {
                    return ApiResponseFactory.Success(0);
                }

                var response = notifications.UserNotifications.Count();
                return ApiResponseFactory.Success(response);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<int>($"Error retrieving unread count: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<List<NotificationDto>>> GetUserNotifications(int userId)
        {
            try
            {
                var notifications = await _context.UserNotifications
                .Where(un => un.UserId == userId)
                .Include(un => un.Notification)
                .ToListAsync();

                var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);
                return ApiResponseFactory.Success(notificationDtos);
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<List<NotificationDto>>($"Error retrieving user notifications: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> MarkAllAsRead(int userId)
        {
            try
            {
                var notifications = await _context.UserNotifications
               .Where(un => un.UserId == userId && !un.IsRead)
               .ToListAsync();

                if (notifications == null || !notifications.Any())
                {
                    return ApiResponseFactory.NotFound<string>();
                }

                foreach (var notification in notifications)
                {
                    notification.IsRead = true;
                }
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("All notifications marked as read");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>($"Error marking notifications as read: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> MarkAsRead(int notificationId)
        {
            try
            {
                var notification = await _context.UserNotifications.FindAsync(notificationId);
                if (notification == null)
                {
                    return ApiResponseFactory.NotFound<string>();
                }

                notification.IsRead = true;
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("Notification marked as read");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>($"Error marking notification as read: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> SendNotification(int eventId, AddNotifications request)
        {
            try
            {
                var emergencyEvent = await _context.EmergencyEvents.FindAsync(eventId);
                if (emergencyEvent == null)
                {
                    return ApiResponseFactory.NotFound<string>();
                }

                var notification = _mapper.Map<EmergencyNotification>(request);
                notification.EmergencyEventId = eventId;
                _context.EmergencyNotifications.Add(notification);
                await _context.SaveChangesAsync();
                return ApiResponseFactory.Success("Notification sent successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseFactory.Fail<string>($"Error sending notification: {ex.Message}", HttpStatusCode.InternalServerError);
            }
    }
}

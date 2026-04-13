using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Requests;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface INotificationService
    {
        Task<ApiResponse<string>> SendNotification(int eventId, AddNotifications request);
        Task<ApiResponse<string>> MarkAsRead(int notificationId);
        Task<ApiResponse<string>> MarkAllAsRead(int userId);
        Task<ApiResponse<List<NotificationDto>>> GetUserNotifications(int userId);
        Task<ApiResponse<List<NotificationDto>>> GetNotificationsByType(NOTIFICATION_TYPE type);
        Task<ApiResponse<int>> GetUnreadCount(int userId);
    }
}
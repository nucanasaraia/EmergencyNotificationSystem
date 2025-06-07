using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.DTOs
{
    public class BroadcastNotificationDto
    {
        public int EmergencyEventId { get; set; }
        public string Message { get; set; }
        public NOTIFICATION_TYPE NotificationType { get; set; } = NOTIFICATION_TYPE.NONE;
    }
}

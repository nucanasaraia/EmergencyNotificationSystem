using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public NOTIFICATION_TYPE NotificationType { get; set; }
        public DateTime SentTime { get; set; }
        public bool IsRead { get; set; }
        public DELIVERY_STATUS DeliveryStatus { get; set; }
        public int EmergencyEventId { get; set; }
    }
}

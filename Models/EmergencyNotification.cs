using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.Models
{
    public class EmergencyNotification
    {
        public int Id { get; set; }
        public int EmergencyEventId { get; set; } // FK to EmergencyEvent
        public string Message { get; set; }
        public DateTime SentTime { get; set; }
        public NOTIFICATION_TYPE NotificationType { get; set; } = NOTIFICATION_TYPE.NONE;


        public EmergencyEvent EmergencyEvent { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }

    }
}

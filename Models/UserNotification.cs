using EmergencyNotifRespons.Enums.Status;

namespace EmergencyNotifRespons.Models
{
    public class UserNotification
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK to User
        public int NotificationId { get; set; } // FK to EmergencyNotification

        public User User { get; set; }
        public EmergencyNotification Notification { get; set; }

        public bool IsRead { get; set; }
        public DELIVERY_STATUS DeliveryStatus { get; set; } = DELIVERY_STATUS.NONE;

    }
}

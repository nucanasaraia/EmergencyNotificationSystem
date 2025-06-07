using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.Requests
{
    public class AddNotifications
    {
        public int EmergencyEventId { get; set; }
        public string Message { get; set; }
        public DateTime SentTime { get; set; }
        public NOTIFICATION_TYPE NotificationType { get; set; } = NOTIFICATION_TYPE.NONE;
    }
}

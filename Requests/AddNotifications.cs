using EmergencyNotifRespons.Enums.Type;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.Requests
{
    public class AddNotifications
    {
        public string Message { get; set; }
        public NOTIFICATION_TYPE NotificationType { get; set; } = NOTIFICATION_TYPE.NONE;
    }
}

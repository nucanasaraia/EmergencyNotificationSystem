using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.Requests
{
    public class AddUSersNotifications
    {
        public int UserId { get; set; } 
        public int NotificationId { get; set; }  
        public bool IsRead { get; set; } = false;
        public DELIVERY_STATUS DeliveryStatus { get; set; } = DELIVERY_STATUS.NONE;
    }
}

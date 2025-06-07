using EmergencyNotifRespons.Enums.Status;

namespace EmergencyNotifRespons.Requests
{
    public class AddResourceAssignment
    {
        public int ResourceId { get; set; }
        public int EmergencyEventId { get; set; }
        public int AssignedById { get; set; }
        public DateTime? ReturnedTime { get; set; }
        public STATUS2 Status { get; set; } = STATUS2.NONE;
    }
}

using EmergencyNotifRespons.Enums.Status;

namespace EmergencyNotifRespons.Requests
{
    public class AddResourceAssignment
    {
        public int ResourceId { get; set; }
        public int EmergencyEventId { get; set; }
        public int AssignedById { get; set; }
        public DateTime? ReturnedTime { get; set; }
        public RESOURCE_ASSIGNMENT_STATUS Status { get; set; } = RESOURCE_ASSIGNMENT_STATUS.NONE;
    }
}

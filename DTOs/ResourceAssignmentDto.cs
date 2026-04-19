using EmergencyNotifRespons.Enums.Status;

namespace EmergencyNotifRespons.DTOs
{
    public class ResourceAssignmentDto
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int EmergencyEventId { get; set; }
        public DateTime AssignedTime { get; set; }
        public DateTime? ReturnedTime { get; set; }
        public RESOURCE_STATUS Status { get; set; }
    }
}

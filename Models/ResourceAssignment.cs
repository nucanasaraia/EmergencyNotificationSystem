using EmergencyNotifRespons.Enums.Status;

namespace EmergencyNotifRespons.Models
{
    public class ResourceAssignment
    {
        public int Id { get; set; }
        public int ResourceId { get; set; } // FK to Resource
        public int EmergencyEventId { get; set; } // FK to EmergencyEvent
        public int AssignedById { get; set; } // FK to User

        public Resource Resource { get; set; }
        public EmergencyEvent EmergencyEvent { get; set; }
        public User AssignedBy { get; set; }


        public DateTime AssignedTime { get; set; }
        public DateTime? ReturnedTime { get; set; }
        public STATUS2 Status { get; set; } = STATUS2.NONE;
    }
}

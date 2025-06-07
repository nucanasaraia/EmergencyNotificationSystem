using EmergencyNotifRespons.Enums.Status;

namespace EmergencyNotifRespons.Models
{
    public class VolunteerAssignment
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; } // FK to Volunteer
        public int EmergencyEventId { get; set; } // FK to EmergencyEvent
        public int AssignedById { get; set; } // FK to User

        public Volunteer Volunteer { get; set; }
        public EmergencyEvent EmergencyEvent { get; set; }
        public User AssignedBy { get; set; }

        public DateTime AssignedTime { get; set; }
        public DateTime? CompletedTime { get; set; }
        public STATUS3 Status { get; set; } = STATUS3.ACCEPTED;
    }
}

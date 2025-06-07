using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.Models
{
    public class EmergencyEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EVENT_TYPE EVENT_TYPE { get; set; } = EVENT_TYPE.None;
        public int Severity { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal AffectedRadius { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ACTIVITY_STATUS ACTIVITY_STATUS { get; set; } = ACTIVITY_STATUS.ACTIVE;

        public int CreatedById { get; set; } // FK to User

        public User CreatedBy { get; set; }
        public ICollection<EmergencyNotification> Notifications { get; set; }
        public ICollection<ResourceAssignment> ResourceAssignments { get; set; }
        public ICollection<VolunteerAssignment> VolunteerAssignments { get; set; }
    }
}

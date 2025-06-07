using EmergencyNotifRespons.Enums.Status;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EmergencyNotifRespons.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<VolunteerAssignment> VolunteerAssignments { get; set; }
       
        public string skills { get; set; }
        public AVAILABILITY_STATUS AvailabilityStatus { get; set; } = AVAILABILITY_STATUS.AVAILABLE; // "Available", "Unavailable", "OnMission"
        public string EmergencyContactPhone { get; set; }
    }
}

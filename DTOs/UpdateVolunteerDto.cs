using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.DTOs
{
    public class UpdateVolunteerDto
    {
        public ICollection<VolunteerAssignment> VolunteerAssignments { get; set; }
        public string skills { get; set; }
        public AVAILABILITY_STATUS AvailabilityStatus { get; set; } // "Available", "Unavailable", "OnMission"
        public string EmergencyContactPhone { get; set; }
    }
}


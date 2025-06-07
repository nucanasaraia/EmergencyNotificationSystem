using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.Requests
{
    public class AddVolunteer
    {
        public int UserId { get; set; }
        public ICollection<VolunteerAssignment> VolunteerAssignments { get; set; }
        public string skills { get; set; }
        public AVAILABILITY_STATUS AvailabilityStatus { get; set; } // "Available", "Unavailable", "OnMission"
        public string EmergencyContactPhone { get; set; }
    }
}

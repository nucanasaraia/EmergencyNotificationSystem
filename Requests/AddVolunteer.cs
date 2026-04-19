using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.Requests
{
    public class AddVolunteer
    {
        public string Skills { get; set; }
        public AVAILABILITY_STATUS AvailabilityStatus { get; set; } // "Available", "Unavailable", "OnMission"
        public string EmergencyContactPhone { get; set; }
    }
}

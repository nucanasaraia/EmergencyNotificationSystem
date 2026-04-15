using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.DTOs
{
    public class UpdateVolunteerDto
    {
        public string Skills { get; set; }
        public string EmergencyContactPhone { get; set; }
        public AVAILABILITY_STATUS AvailabilityStatus { get; set; }
    }
}


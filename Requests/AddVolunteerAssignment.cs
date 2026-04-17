using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.Requests
{
    public class AddVolunteerAssignment
    {
        public int VolunteerId { get; set; } // FK to Volunteer
        public int EmergencyEventId { get; set; } // FK to EmergencyEvent
        public int AssignedById { get; set; } // FK to User
        public DateTime? CompletedTime { get; set; }
        public VOLUNTEER_ASSIGNMENT_STATUS Status { get; set; } = VOLUNTEER_ASSIGNMENT_STATUS.ACCEPTED;
    }
}

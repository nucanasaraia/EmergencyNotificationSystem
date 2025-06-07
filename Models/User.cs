using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ROLES_TYPE Role { get; set; } = ROLES_TYPE.CITIZEN; //Citizen, Volunteer, EmergencyService, Admin
        public bool IsEmailConfirmed { get; set; }
        public ACCOUNT_STATUS Status { get; set; } = ACCOUNT_STATUS.CODE_SENT;
        public string? VerificationCode { get; set; }
        public string? PasswordResetCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Location { get; set; }

        public ICollection<EmergencyEvent> EmergencyEvents { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<ResourceAssignment> ResourceAssignments { get; set; }
        public ICollection<VolunteerAssignment> VolunteerAssignments { get; set; }

        public Volunteer VolunteerProfile { get; set; } // One-to-One
    }
}

using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.Requests
{
    public class AddUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public ROLES_TYPE Role { get; set; } = ROLES_TYPE.CITIZEN; //Citizen, Volunteer, EmergencyService, Admin
        public DateTime RegistrationDate { get; set; }

    }
}

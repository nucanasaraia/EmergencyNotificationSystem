using EmergencyNotifRespons.Enums.Status;
using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ROLES_TYPE Role { get; set; }
        public string Location { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}

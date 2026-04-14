using EmergencyNotifRespons.Enums.Type;

namespace EmergencyNotifRespons.Requests
{
    public class RegistrationRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public ROLES_TYPE Role { get; set; } = ROLES_TYPE.CITIZEN;
    }
}

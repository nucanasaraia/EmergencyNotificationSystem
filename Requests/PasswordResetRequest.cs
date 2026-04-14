namespace EmergencyNotifRespons.Requests
{
    public class PasswordResetRequest
    {
        public required string Token { get; set; }
        public required string NewPassword { get; set; }
    }
}

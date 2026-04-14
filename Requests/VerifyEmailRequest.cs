namespace EmergencyNotifRespons.Requests
{
    public class VerifyEmailRequest
    {
        public required string Email { get; set; }
        public required string VerificationCode { get; set; }
    }
}

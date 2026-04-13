using EmergencyNotifRespons.CORE;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ApiResponse<string>> SendResetPasswordLink(string toEmail, string userName, string resetLink);
        Task<ApiResponse<string>> SendVerificationCode(string toEmail, string userName, string code);
    }
}

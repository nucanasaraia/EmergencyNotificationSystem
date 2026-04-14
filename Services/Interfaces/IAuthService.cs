using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.DTOs;
using EmergencyNotifRespons.Requests;
using Microsoft.AspNetCore.Identity.Data;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> Registration(RegistrationRequest request);
        Task<ApiResponse<string>> VerifyEmail(VerifyEmailRequest request);
        Task<ApiResponse<AuthResponseDto>> LogIn(LogInRequest request);
        Task<ApiResponse<AuthResponseDto>> RefreshToken(string refreshToken);
        Task<ApiResponse<string>> ForgotPassword(string email);
        Task<ApiResponse<string>> ResetPassword(PasswordResetRequest request);
    }
}

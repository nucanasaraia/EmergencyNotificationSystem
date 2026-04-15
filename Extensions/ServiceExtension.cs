using EmergencyNotifRespons.Services.Implementation;
using EmergencyNotifRespons.Services.Interfaces;

namespace EmergencyNotifRespons.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmergencyEventService, EmergencyEventService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IResourcesService, ResourcesService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserLoggerService, UserLoggerService>();
        }
    }
}

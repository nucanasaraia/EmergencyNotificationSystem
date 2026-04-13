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
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IUserService, MapService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}

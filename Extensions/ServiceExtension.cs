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
            services.AddScoped<IEmergencyNotificService, EmergencyNotificService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IResourcesService, ResourcesService>();
            services.AddScoped<IVolunteerAssignmentService, VolunteerAssignmentService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}

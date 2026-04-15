using EmergencyNotifRespons.Helpers;

namespace EmergencyNotifRespons.Extensions
{
    public static class MappingExtension
    {
        public static void ConfigureMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}

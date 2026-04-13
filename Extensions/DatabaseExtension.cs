using EmergencyNotifRespons.Data;
using Microsoft.EntityFrameworkCore;

namespace EmergencyNotifRespons.Extensions
{
    public static class DatabaseExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services,  IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}

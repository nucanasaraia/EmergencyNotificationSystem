using EmergencyNotifRespons.Data;
using Microsoft.EntityFrameworkCore; 


namespace EmergencyNotifRespons.Extensions
{
    public static class DatabaseExtension
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                }));
        }
    }
}
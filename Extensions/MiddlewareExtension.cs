using EmergencyNotifRespons.Middleware;

namespace EmergencyNotifRespons.Extensions
{
    public static class MiddlewareExtension
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();


            app.UseSwagger();
            app.UseSwaggerUI();


            app.MapControllers();
        }
    }
}

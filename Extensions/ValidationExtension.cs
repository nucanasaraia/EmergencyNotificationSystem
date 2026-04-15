using EmergencyNotifRespons.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace EmergencyNotifRespons.Extensions
{
    public static class ValidationExtension
    {
        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginValidator>();
            services.AddValidatorsFromAssemblyContaining<PasswordResetValidator>();
        }
    }
}

using EmergencyNotifRespons.Requests;
using FluentValidation;

namespace EmergencyNotifRespons.Validations
{
    public class LoginValidator : AbstractValidator<LogInRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}

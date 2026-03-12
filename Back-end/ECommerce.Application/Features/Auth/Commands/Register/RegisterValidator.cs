using FluentValidation;

namespace ECommerce.Application.Features.Auth.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}

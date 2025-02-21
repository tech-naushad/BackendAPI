using FluentValidation;

namespace IdentityManagement.Application.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull()
                .WithMessage("UserName is required");
            RuleFor(x => x.Password).NotNull()
                .WithMessage("Password is required");
        }
    }
}

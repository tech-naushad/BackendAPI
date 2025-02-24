using FluentValidation;

namespace IdentityManagement.Application.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull()
                .MaximumLength(30).MinimumLength(6).WithMessage("Name is required with length between 6 to 30 characters");
            RuleFor(x => x.UserName).NotEmpty().NotNull()
                .MaximumLength(20).MinimumLength(6).WithMessage("UserName is required with length between 6 to 20 characters");
            RuleFor(x => x.Email).NotNull()
                 .MaximumLength(30).MinimumLength(5).WithMessage("Email is required with length between 5 to 30 characters");
            RuleFor(x => x.Password).NotNull()
                .MaximumLength(20).MinimumLength(5).WithMessage("Password is required with length between 5 to 20 characters");
        }
    }
}

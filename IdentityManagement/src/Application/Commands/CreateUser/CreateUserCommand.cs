using APICore.DomainEvents;
using IdentityManagement.Domain.Entities;
using IdentityManagement.Persistence.Repositories.Interfaces;
using MediatR;

namespace IdentityManagement.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserCommandDto>
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserHandler : IRequestHandler<CreateUserCommand, CreateUserCommandDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;


        public RegisterUserHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<CreateUserCommandDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser 
            { 
                UserName = request.UserName, 
                Email = request.Email, 
                Name = request.Name, 
                Password = request.Password 
            };
            var entity =  await _userRepository.CreateAsync(user, cancellationToken);

            //publish the event
            var orderCreatedEvent = new UserCreatedEvent(entity.Name, entity.UserName, entity.Email);
            await _mediator.Publish(orderCreatedEvent);

            return new CreateUserCommandDto { Name = entity.Name, Email = entity.Email, UserName = entity.UserName };
        }
    }
}

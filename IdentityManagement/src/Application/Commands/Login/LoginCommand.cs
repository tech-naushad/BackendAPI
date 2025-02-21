using IdentityManagement.Persistence.Repositories.Interfaces;
using APICore.Services.Token;
using MediatR;
using APICore.Factoty.Token;

namespace IdentityManagement.Application.Commands.Login
{
    public class LoginCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public TokenProviderType ProviderType { get; set; }

    }

    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var entity =  await _userRepository.GetByUserNameOrEmailAsync(command.UserName, command.Password);
            if (entity == null) 
            {
                throw new Exception("User not found");
            }
            return await _tokenService.GetTokenAsync(command.ProviderType, command.UserName);
        }
    }
}

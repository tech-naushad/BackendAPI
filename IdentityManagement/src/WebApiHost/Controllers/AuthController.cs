using IdentityManagement.Application.Commands.Login;
using IdentityManagement.Application.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagement.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMediator _mediator;
        public AuthController(ILogger<AuthController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        {
            var result =  await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Token")]
        public async Task<IActionResult> GenerateToken(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            // Trigger OAuth authentication with GitHub
            var redirectUrl = Url.Action(nameof(Callback), "Account", new { returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "GitHub");
        }
        [HttpGet("signin-github")]
        public async Task<IActionResult> Callback(string returnUrl = null)
        {
            // After authentication, GitHub redirects to this endpoint
            var result = await HttpContext.AuthenticateAsync("GitHub");

            if (!result.Succeeded)
            {
                return BadRequest("GitHub authentication failed.");
            }

            // Access the OAuth token
            var accessToken = result.Properties.GetTokenValue("access_token");

            // You can use this token to call GitHub's API or store it in a session
            return Ok(new { AccessToken = accessToken });
        }
    }
}

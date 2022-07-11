using CallistoDinner.Application.Authentication.Commands.Register;
using CallistoDinner.Application.Authentication.Queries.Login;
using CallistoDinner.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CallistoDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var result = await _mediator.Send(command);
            return Ok(new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token));
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var result = await _mediator.Send(query);
            return Ok(new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token));
        }
    }
}

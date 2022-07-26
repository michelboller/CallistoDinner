using CallistoDinner.Application.Authentication.Commands.PasswordReset;
using CallistoDinner.Application.Authentication.Commands.Register;
using CallistoDinner.Application.Authentication.Queries.Login;
using CallistoDinner.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CallistoDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<AuthenticationResponse>(result);
            return Ok(response);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var result = await _mediator.Send(query);
            var response = _mapper.Map<AuthenticationResponse>(result);
            return Ok(response);
        }

        [HttpGet("requestPasswordReset/{mail}")]
        public async Task<IActionResult> RequestPasswordReset(string mail)
        {
            var command = new RequestPasswordResetCommand(mail);
            var result = await _mediator.Send(command);
            return Ok(result ? "Success" : "Something went wrong!");
        }

        [HttpPost("passwordReset")]
        public async Task<IActionResult> PasswordReset(PasswordResetRequest request)
        {
            var command = new PasswordResetCommand(request.PasswordResetId, request.Email, request.Password);
            var result = await _mediator.Send(command);
            return Ok(result ? "Success" : "Something went wrong!");
        }
    }
}

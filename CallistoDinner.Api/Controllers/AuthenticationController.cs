using CallistoDinner.Application.Authentication.Commands.PasswordReset;
using CallistoDinner.Application.Authentication.Commands.Register;
using CallistoDinner.Application.Authentication.Queries.Login;
using CallistoDinner.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CallistoDinner.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
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
            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var result = await _mediator.Send(query);
            return result.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)),
                errors => Problem(errors));
        }

        [HttpGet("requestPasswordReset/{mail}")]
        public async Task<IActionResult> RequestPasswordReset(string mail)
        {
            var command = new RequestPasswordResetCommand(mail);
            var result = await _mediator.Send(command);
            return result.Match(
                result => Ok(_mapper.Map<RequestPasswordResetResponse>(result)),
                errors => Problem(errors));
            
            //var response = _mapper.Map<RequestPasswordResetResponse>(result);

            //if(response.IsSuccess)
            //    return Ok(response);

            //return Problem(detail: response.Message, statusCode: 400);
        }

        [HttpPost("passwordReset")]
        public async Task<IActionResult> PasswordReset(PasswordResetRequest request)
        {
            var command = _mapper.Map<PasswordResetCommand>(request);
            var result = await _mediator.Send(command);
            return result.Match(
                result => Ok(_mapper.Map<PasswordResetResponse>(result)),
                errors => Problem(errors));

            //var response = _mapper.Map<PasswordResetResponse>(result);

            //if (response.IsSuccess)
            //    return Ok(response);

            //return Problem(detail: response.Message, statusCode: 400);
        }
    }
}

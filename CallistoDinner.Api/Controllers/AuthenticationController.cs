﻿using CallistoDinner.Application.Services.Authentication;
using CallistoDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CallistoDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var result = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            return Ok(new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token));
        }
        
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authenticationService.Login(request.Email, request.Password);
            return Ok(new AuthenticationResponse(result.User.Id, result.User.FirstName, result.User.LastName, result.User.Email, result.Token));
        }
    }
}

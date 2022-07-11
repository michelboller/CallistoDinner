using CallistoDinner.Application.Authentication.Common;
using CallistoDinner.Application.Common.Exceptions;
using CallistoDinner.Application.Common.Interfaces.Authentication;
using CallistoDinner.Application.Common.Interfaces.Persistence;
using CallistoDinner.Domain.Entities;
using MediatR;

namespace CallistoDinner.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            //1. Validate the user doesn't exist
            //2. Create user (generate unique Id) and persist to DB
            //3. Create JWT Token

            if (_userRepository.GetUserByEmail(command.Email) is not null)
                throw new SllException("User with given email already exists.");

            var user = new User { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}

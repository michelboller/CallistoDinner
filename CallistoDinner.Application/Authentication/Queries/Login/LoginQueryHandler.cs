using CallistoDinner.Application.Authentication.Common;
using CallistoDinner.Application.Common.Exceptions;
using CallistoDinner.Application.Common.Interfaces.Authentication;
using CallistoDinner.Application.Common.Interfaces.Persistence;
using CallistoDinner.Domain.Entities;
using MediatR;


namespace CallistoDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //1. Validate the user exists
            //2. Validate the password is correct
            //3. Create JWT token

            if (_userRepository.GetUserByEmail(query.Email) is not User user)
                throw new SllException("Invalid login.");

            if (user.IsPasswordResetRequested)
                throw new SllException("Invalid login.");

            if (user.Password != query.Password)
                throw new SllException("Invalid login.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}

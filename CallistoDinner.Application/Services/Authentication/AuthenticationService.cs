using CallistoDinner.Application.Common.Interfaces.Authentication;
using CallistoDinner.Application.Common.Interfaces.Persistence;
using CallistoDinner.Application.Common.Exceptions;
using CallistoDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            //1. Validate the user exists
            //2. Validate the password is correct
            //3. Create JWT token

            if (_userRepository.GetUserByEmail(email) is not User user)
                throw new SllException("Invalid login.");

            if (user.Password != password)
                throw new SllException("Invalid login.");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //1. Validate the user doesn't exist
            //2. Create user (generate unique Id) and persist to DB
            //3. Create JWT Token

            if (_userRepository.GetUserByEmail(email) is not null)
                throw new SllException("User with given email already exists.");

            var user = new User { FirstName = firstName, LastName = lastName, Email = email, Password = password };

            _userRepository.Add(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}

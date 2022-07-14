using CallistoDinner.Application.Common.Exceptions;
using CallistoDinner.Application.Common.Interfaces.Persistence;
using CallistoDinner.Domain.Entities;
using MediatR;
using PWReset = CallistoDinner.Domain.Entities.PasswordReset;

namespace CallistoDinner.Application.Authentication.Commands.PasswordReset
{
    public class PasswordResetHandler :
        IRequestHandler<RequestPasswordResetCommand, bool>,
        IRequestHandler<PasswordResetCommand, bool>
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetRepository _passwordResetRepository;

        public PasswordResetHandler(IUserRepository userRepository, IPasswordResetRepository passwordResetRepository)
        {
            _userRepository = userRepository;
            _passwordResetRepository = passwordResetRepository;
        }

        public async Task<bool> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(request.Email) is not User user)
                throw new SllException("Invalid request");

            if (user.IsPasswordResetRequested)
                throw new SllException("Password reset is already required.");

            _userRepository.RequestedPasswordReset(user);

            var passwordReset = new PWReset { UserId = user.Id };

            _passwordResetRepository.Add(passwordReset);

            return true;
        }

        public async Task<bool> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            if (_passwordResetRepository.GetPasswordResetById(request.PasswordResetId) is not PWReset passwordReset)
                throw new SllException("Invalid request.");

            if (_userRepository.GetUserByEmail(request.Email) is not User user)
                throw new SllException("Invalid request");

            if (passwordReset.UserId != user.Id)
                throw new SllException("Invalid request.");

            if (!user.IsPasswordResetRequested)
                throw new SllException("Invalid request.");

            _passwordResetRepository.ResetPassword(passwordReset, user, request.Password);

            return true;
        }
    }
}

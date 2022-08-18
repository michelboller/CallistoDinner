using CallistoDinner.Application.Authentication.Common;
using CallistoDinner.Application.Common.Interfaces.Persistence;
using CallistoDinner.Domain.Entities;
using MediatR;
using ErrorOr;
using PWReset = CallistoDinner.Domain.Entities.PasswordReset;
using Errors = CallistoDinner.Domain.Common.Errors.Errors;

namespace CallistoDinner.Application.Authentication.Commands.PasswordReset
{
    public class PasswordResetHandler :
        IRequestHandler<RequestPasswordResetCommand, ErrorOr<RequestPasswordResetResult>>,
        IRequestHandler<PasswordResetCommand, ErrorOr<PasswordResetResult>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetRepository _passwordResetRepository;

        public PasswordResetHandler(IUserRepository userRepository, IPasswordResetRepository passwordResetRepository)
        {
            _userRepository = userRepository;
            _passwordResetRepository = passwordResetRepository;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ErrorOr<RequestPasswordResetResult>> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(request.Email) is not User user)
                return Errors.Authentication.InvalidCredentials;

            if (user.IsPasswordResetRequested)
                return Errors.PasswordReset.PasswordResetAlreadyRequired;

            _userRepository.RequestedPasswordReset(user);

            var passwordReset = new PWReset { UserId = user.Id };

            _passwordResetRepository.Add(passwordReset);

            return new RequestPasswordResetResult(user.Id, passwordReset.Id, true);
        }

        public async Task<ErrorOr<PasswordResetResult>> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            if (_passwordResetRepository.GetPasswordResetById(request.PasswordResetId) is not PWReset passwordReset)
                return Errors.Authentication.InvalidCredentials;

            if (_userRepository.GetUserByEmail(request.Email) is not User user)
                return Errors.Authentication.InvalidCredentials;

            if (passwordReset.UserId != user.Id)
                return Errors.Authentication.InvalidCredentials;

            if (!user.IsPasswordResetRequested)
                return Errors.Authentication.InvalidCredentials;

            _passwordResetRepository.ResetPassword(passwordReset, user, request.Password);

            return new PasswordResetResult(user.Email, true);
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}

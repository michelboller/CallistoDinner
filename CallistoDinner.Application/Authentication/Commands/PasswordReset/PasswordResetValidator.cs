using FluentValidation;

namespace CallistoDinner.Application.Authentication.Commands.PasswordReset
{
    public class RequestPasswordResetValidator : AbstractValidator<RequestPasswordResetCommand>
    {
        public RequestPasswordResetValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }

    public class PasswordResetValidator : AbstractValidator<PasswordResetCommand>
    {
        public PasswordResetValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PasswordResetId).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

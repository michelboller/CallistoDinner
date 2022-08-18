using CallistoDinner.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CallistoDinner.Application.Authentication.Commands.PasswordReset
{
    public record RequestPasswordResetCommand(string Email) : IRequest<ErrorOr<RequestPasswordResetResult>>;
}

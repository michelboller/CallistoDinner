using CallistoDinner.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CallistoDinner.Application.Authentication.Commands.PasswordReset
{
    public record PasswordResetCommand(Guid PasswordResetId, string Email, string Password) : IRequest<ErrorOr<PasswordResetResult>>;
}

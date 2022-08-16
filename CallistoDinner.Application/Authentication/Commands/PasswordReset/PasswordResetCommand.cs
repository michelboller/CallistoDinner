using CallistoDinner.Application.Authentication.Common;
using MediatR;

namespace CallistoDinner.Application.Authentication.Commands.PasswordReset
{
    public record PasswordResetCommand(Guid PasswordResetId, string Email, string Password) : IRequest<PasswordResetResult>;
}

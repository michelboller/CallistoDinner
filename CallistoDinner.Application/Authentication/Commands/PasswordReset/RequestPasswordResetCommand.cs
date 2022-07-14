using MediatR;

namespace CallistoDinner.Application.Authentication.Commands.PasswordReset
{
    public record RequestPasswordResetCommand(string Email) : IRequest<bool>;
}

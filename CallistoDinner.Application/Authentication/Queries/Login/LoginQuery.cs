using CallistoDinner.Application.Authentication.Common;
using MediatR;

namespace CallistoDinner.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResult>;
}

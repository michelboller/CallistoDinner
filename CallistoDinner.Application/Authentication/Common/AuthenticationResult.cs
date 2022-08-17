using CallistoDinner.Application.Common;
using CallistoDinner.Domain.Entities;

namespace CallistoDinner.Application.Authentication.Common
{
    public record AuthenticationResult(User User, string Token) : HandlerResult;
}

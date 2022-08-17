using CallistoDinner.Application.Common;

namespace CallistoDinner.Application.Authentication.Common
{
    public record PasswordResetResult(string Email, bool IsSuccess) : HandlerResult
    {
        public string Message => IsSuccess ? "Success" : "Something went wrong";
    }
}

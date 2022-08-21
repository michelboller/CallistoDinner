namespace CallistoDinner.Application.Authentication.Common
{
    public record RequestPasswordResetResult(Guid UserId, Guid PasswordResetId, bool IsSuccess)
    {
        public string Message => IsSuccess ? "Success" : "Something went wrong";
    }
}

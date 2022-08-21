namespace CallistoDinner.Application.Authentication.Common
{
    public record PasswordResetResult(string Email, bool IsSuccess)
    {
        public string Message => IsSuccess ? "Success" : "Something went wrong";
    }
}

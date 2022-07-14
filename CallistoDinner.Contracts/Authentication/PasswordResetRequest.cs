
namespace CallistoDinner.Contracts.Authentication
{
    public record PasswordResetRequest(Guid PasswordResetId, string Email, string Password);
}

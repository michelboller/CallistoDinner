using CallistoDinner.Domain.Entities;

namespace CallistoDinner.Application.Common.Interfaces.Persistence
{
    public interface IPasswordResetRepository
    {
        void Add(PasswordReset passwordReset);
        PasswordReset? GetPasswordResetById(Guid id);
        void ResetPassword(PasswordReset passwordReset, User user, string newPassword);
    }
}

using CallistoDinner.Application.Common.Interfaces.Persistence;
using CallistoDinner.Domain.Entities;

namespace CallistoDinner.Infrastructure.Persistence
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly static List<PasswordReset> _passwordResetList = new List<PasswordReset>();

        public void Add(PasswordReset passwordReset)
        {
            _passwordResetList.Add(passwordReset);
        }

        public PasswordReset? GetPasswordResetById(Guid id)
        {
            return _passwordResetList.FirstOrDefault(x => x.Id == id);
        }

        public void ResetPassword(PasswordReset passwordReset, User user, string newPassword)
        {
            user.Password = newPassword;
            user.IsPasswordResetRequested = false;

            _passwordResetList.Remove(passwordReset);
        }
    }
}

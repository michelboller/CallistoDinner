using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class PasswordReset
        {
            public static Error PasswordResetAlreadyRequired => Error.Conflict(code: "PWR.AlreadyRequired", description: "Password reset is already required.");
        }
    }
}

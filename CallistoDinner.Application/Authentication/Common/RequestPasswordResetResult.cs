using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Application.Authentication.Common
{
    public record RequestPasswordResetResult(Guid UserId, Guid PasswordResetId, bool IsSuccess)
    {
        public string Message => IsSuccess ? "Success" : "Something went wrong";
    }
}

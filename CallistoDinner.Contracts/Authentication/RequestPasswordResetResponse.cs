using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Contracts.Authentication
{
    public record RequestPasswordResetResponse(Guid UserId, Guid PasswordResetId, bool IsSuccess, string Message);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Contracts.Authentication
{
    public record PasswordResetResponse(string Email, bool IsSuccess, string Message);
}

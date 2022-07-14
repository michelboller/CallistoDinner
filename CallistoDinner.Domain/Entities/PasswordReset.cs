using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Domain.Entities
{
    public class PasswordReset
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = default;
    }
}

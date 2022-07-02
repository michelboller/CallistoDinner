using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Application.Common.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Application.Common.Exceptions
{
    public class SllException : BaseException
    {
        public SllException()
        {
        }

        public SllException(string message)
            : base(message)
        {
        }

        public SllException(string message, string innerException)
            : base(message, new Exception(innerException))
        {
        }

        public SllException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

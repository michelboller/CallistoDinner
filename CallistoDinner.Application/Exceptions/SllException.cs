using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Application.Exceptions
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
            : base(message, new System.Exception(innerException))
        {
        }

        public SllException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected SllException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

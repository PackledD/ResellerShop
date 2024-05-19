using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public abstract class LogicException : Exception
    {
        public LogicException()
        {
        }

        public LogicException(string? message) : base(message)
        {
            Logger.Logger.Error(message);
        }

        public LogicException(string? message, Exception? innerException) : base(message, innerException)
        {
            Logger.Logger.Error(message);
        }

        protected LogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

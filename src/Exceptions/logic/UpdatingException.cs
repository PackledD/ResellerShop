using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class UpdatingException : LogicException
    {
        public UpdatingException()
        {
        }

        public UpdatingException(string? message) : base(message)
        {
        }

        public UpdatingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UpdatingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

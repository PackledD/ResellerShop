using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class CreationException : LogicException
    {
        public CreationException()
        {
        }

        public CreationException(string? message) : base(message)
        {
        }

        public CreationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

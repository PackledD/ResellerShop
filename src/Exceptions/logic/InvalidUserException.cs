using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class InvalidUserException : LogicException
    {
        public InvalidUserException()
        {
        }

        public InvalidUserException(string? message) : base(message)
        {
        }

        public InvalidUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public InvalidUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

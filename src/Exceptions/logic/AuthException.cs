using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class AuthException : LogicException
    {
        public AuthException()
        {
        }

        public AuthException(string? message) : base(message)
        {
        }

        public AuthException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AuthException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

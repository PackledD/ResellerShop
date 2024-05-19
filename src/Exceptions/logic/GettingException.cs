using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class GettingException : LogicException
    {
        public GettingException()
        {
        }

        public GettingException(string? message) : base(message)
        {
        }

        public GettingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GettingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

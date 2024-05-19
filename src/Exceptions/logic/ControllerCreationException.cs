using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class ControllerCreationException : LogicException
    {
        public ControllerCreationException()
        {
        }

        public ControllerCreationException(string? message) : base(message)
        {
        }

        public ControllerCreationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ControllerCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

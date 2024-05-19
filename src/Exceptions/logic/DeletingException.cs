using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class DeletingException : LogicException
    {
        public DeletingException()
        {
        }

        public DeletingException(string? message) : base(message)
        {
        }

        public DeletingException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DeletingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

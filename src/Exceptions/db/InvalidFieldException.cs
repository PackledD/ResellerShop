using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.db
{
    internal class InvalidFieldException : DbException
    {
        public InvalidFieldException()
        {
        }

        public InvalidFieldException(string? message) : base(message)
        {
        }

        public InvalidFieldException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public InvalidFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

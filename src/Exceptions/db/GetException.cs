using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.db
{
    public class GetException : DbException
    {
        public GetException()
        {
        }

        public GetException(string? message) : base(message)
        {
        }

        public GetException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public GetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

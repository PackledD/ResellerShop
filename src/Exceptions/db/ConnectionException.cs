using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.db
{
    public class ConnectionException : DbException
    {
        public ConnectionException()
        {
        }

        public ConnectionException(string? message) : base(message)
        {
        }

        public ConnectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public ConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

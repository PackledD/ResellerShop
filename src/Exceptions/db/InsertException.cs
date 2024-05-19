using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.db
{
    public class InsertException : DbException
    {
        public InsertException()
        {
        }

        public InsertException(string? message) : base(message)
        {
        }

        public InsertException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public InsertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

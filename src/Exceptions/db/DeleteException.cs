using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.db
{
    public class DeleteException : DbException
    {
        public DeleteException()
        {
        }

        public DeleteException(string? message) : base(message)
        {
        }

        public DeleteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public DeleteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

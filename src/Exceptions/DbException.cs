using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public abstract class DbException : Exception
    {
        public DbException()
        {
        }

        public DbException(string? message) : base(message)
        {
            Logger.Logger.Error(message);
        }

        public DbException(string? message, Exception? innerException) : base(message, innerException)
        {
            Logger.Logger.Error(message);
        }

        protected DbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

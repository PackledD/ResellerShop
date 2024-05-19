using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.db
{
    public class CmdException : DbException
    {
        public CmdException()
        {
        }

        public CmdException(string? message) : base(message)
        {
        }

        public CmdException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public CmdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

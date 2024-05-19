using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.db
{
    public class ContractException : DbException
    {
        public ContractException()
        {
        }

        public ContractException(string? message) : base(message)
        {
        }

        public ContractException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public ContractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class ContractException : LogicException
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

        protected ContractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

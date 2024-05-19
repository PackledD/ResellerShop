using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.db
{
    public class ValidatorException : DbException
    {
        public ValidatorException()
        {
        }

        public ValidatorException(string? message) : base(message)
        {
        }

        public ValidatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public ValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

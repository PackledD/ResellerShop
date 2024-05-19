using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace reseller_shop_tests.unit_tests
{
    internal class TestException : DbException
    {
        public TestException()
        {
        }

        public TestException(string? message) : base(message)
        {
        }

        public TestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public TestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

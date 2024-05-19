using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class ConfigException : Exception
    {
        public ConfigException()
        {
        }

        public ConfigException(string? message) : base(message)
        {
            Logger.Logger.Error(message);
        }

        public ConfigException(string? message, Exception? innerException) : base(message, innerException)
        {
            Logger.Logger.Error(message);
        }

        protected ConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

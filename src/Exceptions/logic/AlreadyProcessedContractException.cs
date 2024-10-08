﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.logic
{
    public class AlreadyProcessedContractException : LogicException
    {
        public AlreadyProcessedContractException()
        {
        }

        public AlreadyProcessedContractException(string? message) : base(message)
        {
        }

        public AlreadyProcessedContractException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public AlreadyProcessedContractException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

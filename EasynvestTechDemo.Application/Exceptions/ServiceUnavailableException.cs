using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EasynvestTechDemo.Application.Exceptions
{
    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException()
        {
        }

        public ServiceUnavailableException(string message) : base(message)
        {
        }

        public ServiceUnavailableException(string message, object details) : base(message)
        {
            Data.Add("Details", details);
        }

        public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

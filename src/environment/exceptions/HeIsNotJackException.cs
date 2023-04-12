using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JackNTFS.src.environment.exceptions
{
    internal class HeIsNotJackException : ArgumentNullException
    {
        public HeIsNotJackException() {}

        public HeIsNotJackException(string? paramName)
            : base(paramName) {}

        public HeIsNotJackException(string? message, Exception? innerException)
            : base(message, innerException) {}

        public HeIsNotJackException(string? paramName, string? message)
            : base(paramName, message) {}

        protected HeIsNotJackException(SerializationInfo info, StreamingContext context)
            : base(info, context) {}
    }
}

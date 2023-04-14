using JackNTFS.src.userinterface.exports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using static JackNTFS.src.userinterface.exports.WilliamLogger.WPriority;
using static JackNTFS.src.userinterface.exports.WilliamLogger.WPurpose;

namespace JackNTFS.src.environment.exceptions
{
    public class HeIsNotJacksException : Exception
    {
        private readonly WilliamLogger wLogger;

        public HeIsNotJacksException()
        {
            this.wLogger = new WilliamLogger(SERIOUS, EXCEPTION);
        }

        public HeIsNotJacksException(string? message) : base(message)
        {
            message ??= $"{nameof(HeIsNotJacksException)} was thrown.";

            wLogger.Log(wLogger, new object[] { message });
        }

        public HeIsNotJacksException(string? message, Exception? innerException) : base(message, innerException)
        {
            message ??= $"{nameof(HeIsNotJacksException)} was thrown.";

            wLogger.Log(wLogger, new object[] { message });
        }

        protected HeIsNotJacksException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

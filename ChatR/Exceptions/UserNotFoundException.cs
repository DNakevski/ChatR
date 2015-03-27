using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ChatR.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base() { }

        public UserNotFoundException(string message) : base(message) { }

        public UserNotFoundException(string format, params object[] args) : base(String.Format(format, args)) { }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
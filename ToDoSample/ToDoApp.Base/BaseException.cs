using System;
using System.Runtime.Serialization;

namespace ToDoApp.Base
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public BaseException(string message, Exception innerException) : base(message, innerException) { }
        public BaseException(string message) : base(message) { }
        public BaseException() : base() { }
    }
}

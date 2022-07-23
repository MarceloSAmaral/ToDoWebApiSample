using System;
using System.Runtime.Serialization;

namespace ToDoApp.Base
{
    [Serializable]
    public class UserNotFoundException : BaseException
    {
        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException() : base() { }
    }
}

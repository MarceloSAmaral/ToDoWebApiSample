using System;
using System.Runtime.Serialization;

namespace ToDoApp.Base
{
    [Serializable]
    public class ToDoItemNotFoundException : BaseException
    {
        protected ToDoItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public ToDoItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public ToDoItemNotFoundException(string message) : base(message) { }
        public ToDoItemNotFoundException() : base() { }
    }
}

using System;
using System.Runtime.Serialization;

namespace ToDoApp.Base
{
    [Serializable]
    public class ToDoItemCannotBeCreatedAlreadyCompletedException : BaseException
    {
        protected ToDoItemCannotBeCreatedAlreadyCompletedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public ToDoItemCannotBeCreatedAlreadyCompletedException(string message, Exception innerException) : base(message, innerException) { }
        public ToDoItemCannotBeCreatedAlreadyCompletedException(string message) : base(message) { }
        public ToDoItemCannotBeCreatedAlreadyCompletedException() : base() { }
    }
}

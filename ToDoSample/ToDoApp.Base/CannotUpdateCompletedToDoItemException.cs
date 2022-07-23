using System;
using System.Runtime.Serialization;

namespace ToDoApp.Base
{
    [Serializable]
    public class CannotUpdateCompletedToDoItemException : Exception
    {
        public CannotUpdateCompletedToDoItemException()
        {
        }

        public CannotUpdateCompletedToDoItemException(string message) : base(message)
        {
        }

        public CannotUpdateCompletedToDoItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotUpdateCompletedToDoItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

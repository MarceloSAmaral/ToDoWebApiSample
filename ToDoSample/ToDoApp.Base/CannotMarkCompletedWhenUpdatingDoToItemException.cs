using System;
using System.Runtime.Serialization;

namespace ToDoApp.Base
{
    [Serializable]
    public class CannotMarkCompletedWhenUpdatingDoToItemException : BaseException
    {
        public CannotMarkCompletedWhenUpdatingDoToItemException()
        {
        }

        public CannotMarkCompletedWhenUpdatingDoToItemException(string message) : base(message)
        {
        }

        public CannotMarkCompletedWhenUpdatingDoToItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotMarkCompletedWhenUpdatingDoToItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

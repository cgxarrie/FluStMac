namespace Cgxarrie.Flow.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ActionNotPermittedException : Exception
    {
        private string _actionName;
        private string _status;

        public ActionNotPermittedException(string status, string actionName)
            : base($"Action {actionName} not permitted in status {status}")
        {
            _status = status;
            _actionName = actionName;
        }

        private ActionNotPermittedException()
        {
        }

        private ActionNotPermittedException(string? message) : base(message)
        {
        }

        private ActionNotPermittedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        private ActionNotPermittedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
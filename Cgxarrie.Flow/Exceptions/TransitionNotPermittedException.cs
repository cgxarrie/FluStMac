namespace Cgxarrie.Flow.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class TransitionNotPermittedException : Exception
    {
        private string _actionName;
        private string _status;
        private string _targetStatus;

        public TransitionNotPermittedException(string status, string actionName, string targetStatus)
            : base($"Transition {status}.{actionName} -> {targetStatus} not permitted ")
        {
            _status = status;
            _actionName = actionName;
            _targetStatus = targetStatus;
        }

        private TransitionNotPermittedException()
        {
        }

        private TransitionNotPermittedException(string? message) : base(message)
        {
        }

        private TransitionNotPermittedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        private TransitionNotPermittedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
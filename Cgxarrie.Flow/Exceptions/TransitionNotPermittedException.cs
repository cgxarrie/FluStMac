namespace Cgxarrie.Flow.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class TransitionNotPermittedException : ApplicationException
    {
        private string _newStatus;
        private string _status;

        public TransitionNotPermittedException(string status, string newStatus)
            : base($"Transitionnot permitted from {status} to {newStatus}")
        {
            _status = status;
            _newStatus = newStatus;
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
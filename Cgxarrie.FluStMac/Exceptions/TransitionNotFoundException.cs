namespace Cgxarrie.FluStMac.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class TransitionNotFoundException : Exception
    {
        private string _actionName;
        private string _status;

        public TransitionNotFoundException(string status, string actionName)
            : base($"No transition found for {status}.{actionName}")
        {
            _status = status;
            _actionName = actionName;
        }

        private TransitionNotFoundException()
        {
        }

        private TransitionNotFoundException(string? message) : base(message)
        {
        }

        private TransitionNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        private TransitionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
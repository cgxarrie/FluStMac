namespace Cgxarrie.Flow
{
    using Cgxarrie.Flow.Collections;
    using Cgxarrie.Flow.Exceptions;
    using System.Runtime.CompilerServices;

    public abstract class StateMachineBase<TStatus>
    {
        private readonly Transitions<TStatus> _transitions = new();

        public StateMachineBase(TStatus defaultStatusValue)
        {
            Status = defaultStatusValue;
            DefineTransitions();
        }

        public TStatus Status { get; private set; }

        protected void AddTransition(TStatus status, string actionName, TStatus targetStatus)
        {
            _transitions.Add(new KeyValuePair<TStatus, string>(status, actionName), targetStatus);
        }

        protected abstract void DefineTransitions();

        protected void MoveNext([CallerMemberName] string actionName = "")
        {
            ValidatePermittedAction(actionName);
            Status = _transitions.GetNext(Status, actionName);
        }

        protected void ValidatePermittedAction([CallerMemberName] string actionName = "")
        {
            var key = new KeyValuePair<TStatus, string>(Status, actionName);
            if (!_transitions.Permitted(key))
                throw new ActionNotPermittedException(Status.ToString(), actionName);
        }
    }
}
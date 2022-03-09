namespace Cgxarrie.Flow
{
    using Cgxarrie.Flow.Collections;
    using Cgxarrie.Flow.Exceptions;
    using System.Runtime.CompilerServices;

    public abstract class FlowBase<TStatus>
    {
        private readonly Actions<TStatus> _actions = new();

        public FlowBase(TStatus defaultStatusValue)
        {
            Status = defaultStatusValue;
            DefinePermittedActions();
        }

        public TStatus Status { get; private set; }

        protected void AddAction(TStatus status, string actionName)
        {
            _actions.Add(status, actionName);
        }

        protected void ChangeStatus(TStatus newStatus)
        {
            Status = newStatus;
        }

        protected abstract void DefinePermittedActions();

        protected void ValidatePermittedAction([CallerMemberName] string callerName = "")
        {
            var actionName = callerName.Split(".").Last();
            if (!_actions.Permitted(Status, actionName))
                throw new ActionNotPermittedException(Status.ToString(), actionName);
        }
    }
}
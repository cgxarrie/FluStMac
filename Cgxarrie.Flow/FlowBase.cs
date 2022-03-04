﻿namespace Cgxarrie.Flow
{
    using Cgxarrie.Flow.Collections;
    using Cgxarrie.Flow.Exceptions;
    using System.Runtime.CompilerServices;

    public abstract class FlowBase<TStatus>
    {
        private readonly Actions<TStatus> _actions = new();
        private readonly Transitions<TStatus> _transitions = new();

        protected FlowBase(TStatus defaultStatusValue)
        {
            AddPermittedActions();
            AddPermittedTransitions();
            Status = defaultStatusValue;
        }

        public TStatus Status { get; private set; }

        protected void AddAction(TStatus status, string actionName)
        {
            _actions.Add(status, actionName);
        }

        protected abstract void AddPermittedActions();

        protected abstract void AddPermittedTransitions();

        protected void AddTransition(TStatus fromStatus, TStatus toStatus)
        {
            _transitions.Add(fromStatus, toStatus);
        }

        protected void ChangeStatus(TStatus newStatus)
        {
            if (!_transitions.Permitted(Status, newStatus))
                throw new TransitionNotPermittedException(Status.ToString(), newStatus.ToString());

            Status = newStatus;
        }

        protected void ValidatePermittedAction([CallerMemberName] string actionName = "")
        {
            if (!_actions.Permitted(Status, actionName))
                throw new ActionNotPermittedException(Status.ToString(), actionName);
        }
    }
}
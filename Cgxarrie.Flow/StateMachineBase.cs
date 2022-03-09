namespace Cgxarrie.Flow
{
    using Cgxarrie.Flow.Collections;
    using Cgxarrie.Flow.Exceptions;
    using Cgxarrie.Flow.Extensions;
    using System.Linq.Expressions;

    public abstract class StateMachineBase<T, TStatus>
    {
        private readonly T _element;
        private readonly Transitions<T, TStatus> _transitions = new();

        public StateMachineBase(T element, TStatus defaultStatusValue)
        {
            _element = element;
            Status = defaultStatusValue;
            DefineTransitions();
        }

        public TStatus Status { get; private set; }

        public void Do(Expression<Action<T>> action)
        {
            string actionName = action.GetName();
            ValidatePermittedAction(actionName);
            action.Compile().Invoke(_element);
            MoveNext(actionName);
        }

        protected void AddTransition(TStatus status, Expression<Action<T>> action, TStatus targetStatus)
        {
            _transitions.Add(new KeyValuePair<TStatus, string>(status, action.GetName()), targetStatus);
        }

        protected abstract void DefineTransitions();

        private void MoveNext(string actionName)
        {
            Status = _transitions.GetNext(Status, actionName);
        }

        private void ValidatePermittedAction(string actionName)
        {
            var key = new KeyValuePair<TStatus, string>(Status, actionName);
            if (!_transitions.Permitted(key))
                throw new ActionNotPermittedException(Status.ToString(), actionName);
        }
    }
}
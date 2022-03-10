namespace Cgxarrie.Flow
{
    using Cgxarrie.Flow.Exceptions;
    using Cgxarrie.Flow.Extensions;
    using Cgxarrie.Flow.Transitions;
    using System.Linq.Expressions;

    public abstract class StateMachineBase<T, TStatus>
    {
        private readonly T _element;
        private readonly TransitionsList<T, TStatus> _transitions = new();

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

        protected void AddTransition(TStatus status, Expression<Action<T>> action, TStatus targetStatus) =>
            _transitions.Add(status, action.GetName(), targetStatus);

        protected void AddTransition(TStatus status, Expression<Action<T>> action,
            Expression<Func<T, bool>> condition, TStatus targetStatus) =>
            _transitions.Add(status, action.GetName(), targetStatus, condition);

        protected abstract void DefineTransitions();

        private void MoveNext(string actionName)
        {
            (var found, var nextStatus) = _transitions.GetNextStatus(Status, actionName, _element);

            if (!found)
                throw new TransitionNotFoundException(Status.ToString(), actionName);

            Status = nextStatus!;
        }

        private void ValidatePermittedAction(string actionName)
        {
            if (!_transitions.Permitted(Status, actionName))
                throw new ActionNotPermittedException(Status.ToString(), actionName);
        }
    }
}
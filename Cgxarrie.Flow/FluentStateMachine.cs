namespace Cgxarrie.Flow
{
    using Cgxarrie.Flow.Exceptions;
    using Cgxarrie.Flow.Extensions;
    using Cgxarrie.Flow.Transitions;
    using System.Linq.Expressions;

    public abstract class FluentStateMachine<T, TStatus>
    {
        private readonly T _element;
        private readonly TransitionsList<T, TStatus> _transitions = new();

        public FluentStateMachine(T element, TStatus defaultStatusValue)
        {
            _element = element;
            Status = defaultStatusValue;
        }

        public TStatus Status { get; private set; }

        public void Do(Expression<Action<T>> action)
        {
            string actionName = action.GetName();
            ValidatePermittedAction(actionName);
            action.Compile().Invoke(_element);
            MoveNext(actionName);
        }

        internal void AddTransition(Transition<T, TStatus> transition) => _transitions.Add(transition);

        protected Transition<T, TStatus> WithTransition()
        {
            var transition = new Transition<T, TStatus>(_transitions.Count + 1);
            _transitions.Add(transition);
            return transition;
        }

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
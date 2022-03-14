namespace Cgxarrie.FluStMac
{
    using Cgxarrie.FluStMac.Exceptions;
    using Cgxarrie.FluStMac.Extensions;
    using Cgxarrie.FluStMac.Transitions;
    using System.Linq.Expressions;

    public abstract class FluentStateMachine<T, TStatus>
        where T : StateMachineElement<TStatus>
    {
        private readonly T _element;
        private readonly TransitionsList<T, TStatus> _transitions = new();

        public FluentStateMachine(T element)
        {
            _element = element;
        }

        public void Do(Expression<Action<T>> action)
        {
            string actionName = action.GetName();
            ValidatePermittedAction(actionName);
            action.Compile().Invoke(_element);
            MoveNext(actionName);
        }

        public IEnumerable<string> ValidActions() => _transitions.ValidActions(_element.Status);

        internal void AddTransition(Transition<T, TStatus> transition) => _transitions.Add(transition);

        protected Transition<T, TStatus> WithTransition()
        {
            var transition = new Transition<T, TStatus>(_transitions.Count + 1);
            _transitions.Add(transition);
            return transition;
        }

        private void MoveNext(string actionName)
        {
            (var found, var nextStatus) = _transitions.GetNextStatus(_element.Status, actionName, _element);

            if (!found)
                throw new TransitionNotFoundException(_element.Status.ToString(), actionName);

            _element.Status = nextStatus!;
        }

        private void ValidatePermittedAction(string actionName)
        {
            if (!_transitions.ValidActions(_element.Status).Contains(actionName))
            {
                throw new ActionNotPermittedException(_element.Status.ToString(), actionName);
            }
        }
    }
}
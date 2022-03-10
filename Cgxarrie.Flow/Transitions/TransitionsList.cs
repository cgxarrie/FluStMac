namespace Cgxarrie.Flow.Transitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class TransitionsList<T, TStatus>
    {
        private IList<Transition<T, TStatus>> _items = new List<Transition<T, TStatus>>();

        public bool Permitted(TStatus fromStatus, string actionName) =>
            _items.Any(x => x.FromStatus.Equals(fromStatus) && x.Action == actionName);

        internal void Add(TStatus fromStatus, string actionName, TStatus toStatus, Expression<Func<T, bool>>? condition = null)
        {
            if (_items.Any(x => x.FromStatus.Equals(fromStatus) && x.ToStatus.Equals(toStatus) &&
                                x.Action == actionName && x.Condition == condition))
                return;

            _items.Add(new Transition<T, TStatus>(_items.Count + 1)
            {
                FromStatus = fromStatus,
                ToStatus = toStatus,
                Action = actionName,
                Condition = condition
            });
        }

        internal (bool, TStatus?) GetNextStatus(TStatus status, string actionName, T element)
        {
            var targets = _items.Where(x => x.FromStatus.Equals(status) && x.Action == actionName);
            foreach (var target in targets)
            {
                if (target.Condition == null || target.Condition.Compile().Invoke(element))
                    return (true, target.ToStatus);
            }

            return (false, default);
        }
    }
}
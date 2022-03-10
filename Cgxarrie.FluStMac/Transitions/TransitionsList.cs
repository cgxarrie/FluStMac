namespace Cgxarrie.FluStMac.Transitions
{
    using System.Collections.Generic;
    using System.Linq;

    public class TransitionsList<T, TStatus>
    {
        private IList<Transition<T, TStatus>> _items = new List<Transition<T, TStatus>>();

        public int Count => _items.Count;

        public bool Permitted(TStatus fromStatus, string actionName) =>
            _items.Any(x => x.FromStatus.Equals(fromStatus) && x.ActionName == actionName);

        internal void Add(Transition<T, TStatus> transition)
        {
            if (_items.Any(x => x.Equals(transition)))
                return;

            _items.Add(transition);
        }

        internal (bool, TStatus?) GetNextStatus(TStatus status, string actionName, T element)
        {
            var targets = _items.Where(x => x.FromStatus.Equals(status) && x.ActionName == actionName);
            foreach (var target in targets.OrderBy(x => x.Order))
            {
                if (target.Condition == null || target.Condition.Compile().Invoke(element))
                    return (true, target.ToStatus);
            }

            return (false, default);
        }
    }
}
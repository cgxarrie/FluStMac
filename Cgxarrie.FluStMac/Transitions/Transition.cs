namespace Cgxarrie.FluStMac.Transitions
{
    using Extensions;
    using System;
    using System.Linq.Expressions;

    public class Transition<T, TStatus> : IEquatable<Transition<T, TStatus>>
    {
        public Transition(int order)
        {
            Order = order;
        }

        internal string ActionName { get; private set; } = string.Empty;
        internal Expression<Func<T, bool>>? Condition { get; private set; } = null;
        internal TStatus FromStatus { get; private set; }
        internal int Order { get; private set; }
        internal TStatus ToStatus { get; private set; }

        public bool Equals(Transition<T, TStatus>? other)
                   => other != null &&
           FromStatus.Equals(other.FromStatus) &&
           ToStatus.Equals(other.ToStatus) &&
           Condition.Equals(other.Condition) &&
           ActionName == other.ActionName;

        public Transition<T, TStatus> From(TStatus status)
        {
            FromStatus = status;
            return this;
        }

        public Transition<T, TStatus> On(Expression<Action<T>> action)
        {
            ActionName = action.GetName();
            return this;
        }

        public Transition<T, TStatus> To(TStatus status)
        {
            ToStatus = status;
            return this;
        }

        public Transition<T, TStatus> When(Expression<Func<T, bool>> condition)
        {
            Condition = condition;
            return this;
        }
    }
}
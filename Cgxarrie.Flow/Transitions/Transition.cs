namespace Cgxarrie.Flow.Transitions
{
    using System;
    using System.Linq.Expressions;

    internal class Transition<T, TStatus> : IEquatable<Transition<T, TStatus>>
    {
        public Transition(int order) => Order = order;

        public string Action { get; internal set; } = string.Empty;
        public Expression<Func<T, bool>>? Condition { get; internal set; } = null;
        public TStatus FromStatus { get; internal set; }
        public int Order { get; private set; }
        public TStatus ToStatus { get; internal set; }

        public bool Equals(Transition<T, TStatus>? other)
            => other != null &&
            FromStatus.Equals(other.FromStatus) &&
            ToStatus.Equals(other.ToStatus) &&
            Condition.Equals(other.Condition) &&
            Action == other.Action;
    }
}
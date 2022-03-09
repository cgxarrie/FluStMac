namespace Cgxarrie.Flow.Collections
{
    using System.Linq.Expressions;

    internal class ConditionalTransitions<T, TStatus>
        : FlowCollection<KeyValuePair<TStatus, string>, KeyValuePair<Expression<Func<T, bool>>, TStatus>>
    {
        internal (bool, TStatus?) GetNext(TStatus status, string actionName, T element)
        {
            var targets = Get(new KeyValuePair<TStatus, string>(status, actionName));
            foreach (var target in targets)
            {
                if (target.Key == null || target.Key.Compile().Invoke(element))
                    return (true, target.Value);
            }

            return (false, default);
        }
    }

    internal class Transitions<T, TStatus> : FlowCollection<KeyValuePair<TStatus, string>, TStatus>
    {
        internal TStatus GetNext(TStatus status, string actionName)
            => Get(new KeyValuePair<TStatus, string>(status, actionName)).First();
    }
}
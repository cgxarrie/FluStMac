namespace Cgxarrie.Flow.Collections
{
    internal class Transitions<T, TStatus> : FlowCollection<KeyValuePair<TStatus, string>, TStatus>
    {
        internal TStatus GetNext(TStatus status, string actionName)
            => Get(new KeyValuePair<TStatus, string>(status, actionName)).First();
    }
}
namespace Cgxarrie.Flow.Collections
{
    public class FlowCollection<TKey, TValue>
    {
        private Dictionary<TKey, IEnumerable<TValue>> _items = new();

        public void Add(TKey key, TValue value)
        {
            IEnumerable<TValue> values;
            if (!_items.TryGetValue(key, out values))
            {
                _items.Add(key, new List<TValue> { value });
                return;
            }

            if (values.Contains(value))
                return;

            _items[key] = values.Append(value); ;
        }

        public bool Permitted(TKey key, TValue value)
        {
            return
                _items.ContainsKey(key)
                && _items.TryGetValue(key, out IEnumerable<TValue> values)
                && values.Contains(value);
        }

        public bool Permitted(TKey key) => _items.ContainsKey(key);

        protected IEnumerable<TValue> Get(TKey key) =>
            _items.TryGetValue(key, out IEnumerable<TValue> values)
                ? values
                : new List<TValue>();
    }
}
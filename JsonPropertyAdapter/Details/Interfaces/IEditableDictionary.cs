namespace JsonPropertyAdapter.Details.Interfaces
{
    public interface IEditableDictionary<TKey, TValue>
    {
        public void Edit(Func<IDictionary<TKey, TValue>, IDictionary<TKey, TValue>> EditingAction);

        public void Edit(Action<IDictionary<TKey, TValue>> EditingAction);

        public void AddRange(IDictionary<TKey, TValue> items);

        public void Add(TKey key, TValue item);
    }
}
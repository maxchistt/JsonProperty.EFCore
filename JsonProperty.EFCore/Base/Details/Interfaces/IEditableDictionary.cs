namespace JsonProperty.EFCore.Base.Details.Interfaces
{
    public interface IEditableDictionary<TKey, TValue> : IEditableEnumerable<KeyValuePair<TKey, TValue>>
    {
        public void Edit(Func<IDictionary<TKey, TValue>, IDictionary<TKey, TValue>> EditingAction);

        public void Edit(Func<IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>> EditingAction);

        public void AddRange(IDictionary<TKey, TValue> items);

        public void Add(TKey key, TValue item);
    }
}
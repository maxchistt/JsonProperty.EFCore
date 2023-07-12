using JsonPropertyAdapter.Base.Details.Interfaces;

namespace JsonPropertyAdapter.Base.Interfaces
{
    public interface ISerializibleDictionary<TKey, TValue> : IJsonDictionarySerialize<TKey, TValue>, IEditableDictionary<TKey, TValue>
    {
        public IDictionary<TKey, TValue> VirtualDictionary { get; set; }
    }
}
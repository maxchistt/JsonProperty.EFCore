using JsonProperty.EFCore.Base.Details.Interfaces;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleDictionary<TKey, TValue> : IJsonDictionarySerialize<TKey, TValue>, IEditableDictionary<TKey, TValue>
    {
        public IDictionary<TKey, TValue> VirtualDictionary { get; set; }
    }
}
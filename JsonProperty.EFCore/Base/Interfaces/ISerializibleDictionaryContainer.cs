using JsonProperty.EFCore.Base.Interfaces.Editable;
using JsonProperty.EFCore.Base.Interfaces.Serializible;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleDictionaryContainer<TKey, TValue> : ISerializibleDictionary<TKey, TValue>, IEditableDictionary<TKey, TValue>
    {
        public IDictionary<TKey, TValue> VirtualDictionary { get; set; }
    }
}
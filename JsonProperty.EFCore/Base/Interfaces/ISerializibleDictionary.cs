using JsonProperty.EFCore.Base.Interfaces.Editable;
using JsonProperty.EFCore.Base.Interfaces.Serializers;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleDictionary<TKey, TValue> : IJsonDictionarySerializer<TKey, TValue>, IEditableDictionary<TKey, TValue>
    {
        public IDictionary<TKey, TValue> VirtualDictionary { get; set; }
    }
}
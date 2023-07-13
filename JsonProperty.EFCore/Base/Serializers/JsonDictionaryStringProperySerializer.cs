using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using JsonProperty.EFCore.Base.Interfaces.Serializers;
using JsonProperty.EFCore.Base.Serializers.Base;
using JsonProperty.EFCore.Base.Serializers.CollectionSerializers.Strict;
using JsonProperty.EFCore.Base.Serializers.CollectionSerializers.Unstrict;

namespace JsonProperty.EFCore.Base.Serializers
{
    internal class JsonDictionaryStringPropertySerializer<TKey, TValue> :
        AbstractStringPropertySerializer, IDictionarySerializer<TKey, TValue>
        where TKey : notnull
    {
        private IJsonDictionarySerializer<TKey, TValue> JsonSerializer { get; set; }

        public JsonDictionaryStringPropertySerializer(object parent, string? propName) : base(parent, propName)
        {
            JsonSerializer = UseStrictSerialization
                ? new JsonDictionaryStrictSerializer<TKey, TValue>()
                : new JsonDictionaryUnstrictSerializer<TKey, TValue>();
        }

        public IDictionary<TKey, TValue> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrEmpty(prop))
            {
                var resDict = JsonSerializer.Deserialize(prop);

                if (resDict is not null)
                    return resDict;
            }
            return new Dictionary<TKey, TValue>();
        }

        public void Serialize(IDictionary<TKey, TValue> items)
        {
            string res = JsonSerializer.Serialize(items) ??
                throw new NullReferenceException($"{nameof(Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(Serialize)}");
            SetProp(res);
        }
    }
}
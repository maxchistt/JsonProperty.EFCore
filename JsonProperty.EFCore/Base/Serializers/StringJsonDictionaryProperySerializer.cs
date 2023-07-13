using JsonProperty.EFCore.Base.Interfaces.Serializers;
using JsonProperty.EFCore.Base.Serializers.Base;
using JsonProperty.EFCore.Base.Serializers.CollectionSerializers;

namespace JsonProperty.EFCore.Base.Serializers
{
    internal class StringJsonDictionaryPropertySerializer<TKey, TValue> :
        AbstractStringJsonPropertySerializer, IJsonDictionarySerializer<TKey, TValue>
        where TKey : notnull
    {
        public StringJsonDictionaryPropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public IDictionary<TKey, TValue> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrEmpty(prop))
            {
                var resDict = DictionarySerializer.DeserializeItems<TKey, TValue>(prop);

                if (resDict is not null)
                    return resDict;
            }
            return new Dictionary<TKey, TValue>();
        }

        public void Serialize(IDictionary<TKey, TValue> items)
        {
            string res = DictionarySerializer.SerializeItems<TKey, TValue>(items) ??
                throw new NullReferenceException($"{nameof(Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(Serialize)}");
            SetProp(res);
        }
    }
}
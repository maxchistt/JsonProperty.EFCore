using JsonProperty.EFCore.Base.Interfaces.Serializers;
using JsonProperty.EFCore.Base.JsonTyped;
using JsonProperty.EFCore.Base.Serializers.Base;
using System.Text.Json;

namespace JsonProperty.EFCore.Base.Serializers
{
    internal class StringJsonDictionaryPropertySerializer<TKey, TValue> :
        AbstractStringJsonPropertySerializer, IJsonDictionarySerializer<TKey, TValue>
        where TKey : notnull
    {
        public StringJsonDictionaryPropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public void Serialize(IDictionary<TKey, TValue> items)
        {
            Dictionary<TKey, object[]> dict = new();
            foreach (var item in items)
            {
                object[] valueType = TypePacker.Pack(item.Value);
                dict.Add(item.Key, valueType);
            }
            var serialized = JsonSerializer.Serialize(dict);

            string res = serialized ??
                throw new NullReferenceException($"{nameof(IJsonDictionarySerializer<TKey, TValue>.Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonDictionarySerializer<TKey, TValue>.Serialize)}");
            SetProp(res);
        }

        public IDictionary<TKey, TValue> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrEmpty(prop))
            {
                Dictionary<TKey, TValue>? resDict = null;

                var res = JsonSerializer.Deserialize<IDictionary<TKey, object[]>>(prop);
                if (res is not null)
                {
                    Dictionary<TKey, TValue> dict = new();
                    foreach (var item in res)
                    {
                        object? obj = TypePacker.Unpack(item.Value);
                        TValue val = (TValue)(obj ?? default!);
                        dict.Add(item.Key, val);
                    }
                    resDict = dict;
                }

                if (resDict is not null)
                    return resDict;
            }
            return new Dictionary<TKey, TValue>();
        }
    }
}
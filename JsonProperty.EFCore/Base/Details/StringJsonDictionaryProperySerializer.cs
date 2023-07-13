using JsonProperty.EFCore.Base.Details.Interfaces;
using JsonProperty.EFCore.Base.Details.JsonTyped;
using System.Text.Json;

namespace JsonProperty.EFCore.Base.Details
{
    internal class StringJsonDictionaryPropertySerializer<TKey, TValue> :
        AbstractStringJsonPropertySerializer, IJsonDictionarySerialize<TKey, TValue>
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

            string res = JsonSerializer.Serialize(dict) ??
                throw new NullReferenceException($"{nameof(IJsonDictionarySerialize<TKey, TValue>.Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonDictionarySerialize<TKey, TValue>.Serialize)}");
            SetProp(res);
        }

        public IDictionary<TKey, TValue> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrEmpty(prop))
            {
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

                    return dict;
                }
            }
            return new Dictionary<TKey, TValue>();
        }
    }
}
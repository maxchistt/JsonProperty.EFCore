using JsonPropertyAdapter.Details.Interfaces;
using System.Text.Json;

namespace JsonPropertyAdapter.Details
{
    internal class StringJsonDictionaryPropertySerializer<TKey, TValue> :
        AbstractStringJsonPropertySerializer, IJsonDictionarySerialize<TKey, TValue>
    {
        public StringJsonDictionaryPropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public void JsonDictionarySerialize(IDictionary<TKey, TValue> items)
        {
            string res = JsonSerializer.Serialize(items) ??
                throw new NullReferenceException($"{nameof(IJsonDictionarySerialize<TKey, TValue>.JsonDictionarySerialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonDictionarySerialize<TKey, TValue>.JsonDictionarySerialize)}");
            SetProp(res);
        }

        public IDictionary<TKey, TValue> JsonDictionaryDeserialize()
        {
            var prop = GetProp();
            if (string.IsNullOrEmpty(prop) && prop is not null)
                throw new ArgumentException($"Empty string to get in {nameof(IJsonDictionarySerialize<TKey, TValue>.JsonDictionarySerialize)}");
            IDictionary<TKey, TValue> res = JsonSerializer.Deserialize<IDictionary<TKey, TValue>>(prop ?? "[]") ??
                throw new NullReferenceException($"{nameof(IJsonDictionarySerialize<TKey, TValue>.JsonDictionarySerialize)} get null fail");
            return res;
        }
    }
}
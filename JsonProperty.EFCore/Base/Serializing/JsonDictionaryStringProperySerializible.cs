using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using JsonProperty.EFCore.Base.Interfaces.PropertyProxy;
using JsonProperty.EFCore.Base.Interfaces.Serializible;
using JsonProperty.EFCore.Base.Serializing.JsonSerializers.Strict;
using JsonProperty.EFCore.Base.Serializing.JsonSerializers.Unstrict;
using JsonProperty.EFCore.Base.Serializing.PropertyProxy;

namespace JsonProperty.EFCore.Base.Serializing
{
    internal class JsonDictionaryStringPropertySerializible<TKey, TValue> : ISerializibleDictionary<TKey, TValue> where TKey : notnull
    {
        private IStringPropertyProxy StringProp { get; set; }
        private IJsonDictionarySerializer<TKey, TValue> JsonSerializer { get; set; }

        public JsonDictionaryStringPropertySerializible(object parent, string? propName)
        {
            StringProp = new StringPropertyProxy(parent, propName);
            JsonSerializer = Settings.JsonSettings.StrictTypeSerialization
                ? new JsonDictionaryStrictSerializer<TKey, TValue>()
                : new JsonDictionaryUnstrictSerializer<TKey, TValue>();
        }

        public IDictionary<TKey, TValue> Deserialize()
        {
            var prop = StringProp.Get();
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
            StringProp.Set(res);
        }
    }
}
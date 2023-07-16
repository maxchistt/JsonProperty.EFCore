using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using JsonProperty.EFCore.Base.Interfaces.PropertyProxy;
using JsonProperty.EFCore.Base.Interfaces.Serializible;
using JsonProperty.EFCore.Base.Serializing.JsonSerializers.Strict;
using JsonProperty.EFCore.Base.Serializing.JsonSerializers.Unstrict;
using JsonProperty.EFCore.Base.Serializing.PropertyProxy;

namespace JsonProperty.EFCore.Base.Serializing
{
    internal class JsonItemStringPropertySerializible<T> : ISerializibleItem<T>
    {
        private IStringPropertyProxy StringProp { get; set; }
        private IJsonItemSerializer<T> JsonSerializer { get; set; }

        public JsonItemStringPropertySerializible(object parent, string? propName)
        {
            StringProp = new StringPropertyProxy(parent, propName);
            JsonSerializer = Settings.JsonSettings.StrictTypeSerialization
                ? new JsonItemStrictSerializer<T>()
                : new JsonItemUnstrictSerializer<T>();
        }

        T ISerializibleItem<T>.Deserialize()
        {
            var prop = StringProp.Get();
            if (!string.IsNullOrWhiteSpace(prop))
            {
                var resList = JsonSerializer.Deserialize(prop);

                if (resList is not null)
                    return resList;
            }
            return default;
        }

        public void Serialize(T item)
        {
            string res = JsonSerializer.Serialize(item) ??
                   throw new NullReferenceException($"{nameof(Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(Serialize)}");
            StringProp.Set(res);
        }
    }
}
using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using JsonProperty.EFCore.Base.Interfaces.PropertyProxy;
using JsonProperty.EFCore.Base.Interfaces.Serializible;
using JsonProperty.EFCore.Base.Serializing.JsonSerializers.Strict;
using JsonProperty.EFCore.Base.Serializing.JsonSerializers.Unstrict;
using JsonProperty.EFCore.Base.Serializing.PropertyProxy;

namespace JsonProperty.EFCore.Base.Serializing
{
    internal class JsonArrayStringPropertySerializible<T> : ISerializibleArray<T>
    {
        private IStringPropertyProxy StringProp { get; set; }
        private IJsonArraySerializer<T> JsonSerializer { get; set; }

        public JsonArrayStringPropertySerializible(object parent, string? propName)
        {
            StringProp = new StringPropertyProxy(parent, propName);
            JsonSerializer = Settings.JsonSettings.StrictTypeSerialization
                ? new JsonArrayStrictSerializer<T>()
                : new JsonArrayUnstrictSerializer<T>();
        }

        public IList<T> Deserialize()
        {
            var prop = StringProp.Get();
            if (!string.IsNullOrWhiteSpace(prop))
            {
                var resList = JsonSerializer.Deserialize(prop);

                if (resList is not null)
                    return resList;
            }
            return new T[0].ToList();
        }

        void ISerializibleList<T>.Serialize(IList<T> items)
        {
            Serialize(items);
        }

        IEnumerable<T> ISerializibleEnumerable<T>.Deserialize()
        {
            return Deserialize();
        }

        public void Serialize(IEnumerable<T> items)
        {
            string res = JsonSerializer.Serialize(items) ??
                  throw new NullReferenceException($"{nameof(Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(Serialize)}");
            StringProp.Set(res);
        }
    }
}
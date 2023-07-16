using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using JsonProperty.EFCore.Base.Interfaces.Serializible;
using JsonProperty.EFCore.Base.Serializers.Base;
using JsonProperty.EFCore.Base.Serializers.JsonSerializers.Strict;
using JsonProperty.EFCore.Base.Serializers.JsonSerializers.Unstrict;

namespace JsonProperty.EFCore.Base.Serializers
{
    internal class JsonArrayStringPropertySerializible<T> :
        AbstractStringPropertySerializer, ISerializibleArray<T>
    {
        private IJsonArraySerializer<T> JsonSerializer { get; set; }

        public JsonArrayStringPropertySerializible(object parent, string? propName) : base(parent, propName)
        {
            JsonSerializer = UseStrictSerialization
                ? new JsonArrayStrictSerializer<T>()
                : new JsonArrayUnstrictSerializer<T>();
        }

        public IList<T> Deserialize()
        {
            var prop = GetProp();
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
            SetProp(res);
        }
    }
}
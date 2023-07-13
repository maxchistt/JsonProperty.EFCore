using JsonProperty.EFCore.Base.Interfaces.Serializers;
using JsonProperty.EFCore.Base.Serializers.Base;
using JsonProperty.EFCore.Base.Serializers.CollectionSerializers;

namespace JsonProperty.EFCore.Base.Serializers
{
    internal class StringJsonEnumerablePropertySerializer<T> :
        AbstractStringJsonPropertySerializer, IJsonEnumerableSerializer<T>
    {
        public StringJsonEnumerablePropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public IEnumerable<T> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrWhiteSpace(prop))
            {
                var resList = ArraySerializer.DeserializeItems<T>(prop);

                if (resList is not null)
                    return resList;
            }
            return new T[0].ToList();
        }

        public void Serialize(IEnumerable<T> items)
        {
            string res = ArraySerializer.SerializeItems<T>(items) ??
                  throw new NullReferenceException($"{nameof(Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(Serialize)}");
            SetProp(res);
        }
    }
}
using JsonProperty.EFCore.Base.Details.Interfaces;
using System.Text.Json;

namespace JsonProperty.EFCore.Base.Details
{
    internal class StringJsonEnumerablePropertySerializer<T> :
        AbstractStringJsonPropertySerializer, IJsonEnumerableSerialize<T>
    {
        public StringJsonEnumerablePropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public IEnumerable<T> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrWhiteSpace(prop))
            {
                var res = JsonSerializer.Deserialize<IEnumerable<T>>(prop);
                if (res is not null) return res;
            }
            return new T[0];
        }

        public void Serialize(IEnumerable<T> items)
        {
            string res = JsonSerializer.Serialize(items) ??
                  throw new NullReferenceException($"{nameof(IJsonEnumerableSerialize<T>.Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonEnumerableSerialize<T>.Serialize)}");
            SetProp(res);
        }
    }
}
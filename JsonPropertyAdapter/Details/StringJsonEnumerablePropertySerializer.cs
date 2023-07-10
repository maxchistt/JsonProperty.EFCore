using JsonPropertyAdapter.Details.Interfaces;
using System.Text.Json;

namespace JsonPropertyAdapter.Details
{
    internal class StringJsonEnumerablePropertySerializer<T> :
        AbstractStringJsonPropertySerializer, IJsonEnumerableSerialize<T>
    {
        public StringJsonEnumerablePropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public IEnumerable<T> JsonEnumerableDeserialize()
        {
            var prop = GetProp();
            if (string.IsNullOrEmpty(prop) && prop is not null)
                throw new ArgumentException($"Empty string to get in {nameof(IJsonEnumerableSerialize<T>.JsonEnumerableSerialize)}");
            IEnumerable<T> res = JsonSerializer.Deserialize<IEnumerable<T>>(prop ?? "[]") ??
                throw new NullReferenceException($"{nameof(IJsonEnumerableSerialize<T>.JsonEnumerableSerialize)} get null fail");
            return res;
        }

        public void JsonEnumerableSerialize(IEnumerable<T> items)
        {
            string res = JsonSerializer.Serialize(items) ??
                  throw new NullReferenceException($"{nameof(IJsonEnumerableSerialize<T>.JsonEnumerableSerialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonEnumerableSerialize<T>.JsonEnumerableSerialize)}");
            SetProp(res);
        }
    }
}
using JsonPropertyAdapter.Base.Details.Interfaces;
using System.Text.Json;

namespace JsonPropertyAdapter.Base.Details
{
    internal class StringJsonListPropertySerializer<T> :
        AbstractStringJsonPropertySerializer, IJsonListSerialize<T>
    {
        public StringJsonListPropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public IList<T> JsonListDeserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrWhiteSpace(prop))
            {
                var res = JsonSerializer.Deserialize<IList<T>>(prop);
                if (res is not null) return res;
            }
            return new T[0];
        }

        public void JsonListSerialize(IList<T> items)
        {
            string res = JsonSerializer.Serialize(items) ??
                  throw new NullReferenceException($"{nameof(IJsonListSerialize<T>.JsonListSerialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonListSerialize<T>.JsonListSerialize)}");
            SetProp(res);
        }
    }
}
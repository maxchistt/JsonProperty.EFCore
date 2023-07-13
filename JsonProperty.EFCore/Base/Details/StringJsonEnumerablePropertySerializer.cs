using JsonProperty.EFCore.Base.Details.Interfaces;
using JsonProperty.EFCore.Base.Details.JsonTyped;
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
                var res = JsonSerializer.Deserialize<IEnumerable<object[]>>(prop);
                if (res is not null)
                {
                    int count = res.Count();
                    T[] arr = new T[count];
                    for (int i = 0; i < count; i++)
                    {
                        object? obj = TypePacker.Unpack(res.ElementAt(i));
                        arr[i] = (T)(obj ?? default!);
                    }
                    return arr.ToList();
                }
            }
            return new T[0].ToList();
        }

        public void Serialize(IEnumerable<T> items)
        {
            int count = items.Count();

            List<object[]> list = new List<object[]>(count);
            for (int i = 0; i < count; i++)
            {
                object[] valueType = TypePacker.Pack(items.ElementAt(i));
                list.Insert(i, valueType);
            }

            string res = JsonSerializer.Serialize(list) ??
                  throw new NullReferenceException($"{nameof(IJsonEnumerableSerialize<T>.Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonEnumerableSerialize<T>.Serialize)}");
            SetProp(res);
        }
    }
}
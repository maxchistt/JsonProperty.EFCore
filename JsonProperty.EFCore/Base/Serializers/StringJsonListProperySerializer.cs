using JsonProperty.EFCore.Base.Interfaces.Serializers;
using JsonProperty.EFCore.Base.JsonTyped;
using JsonProperty.EFCore.Base.Serializers.Base;
using System.Text.Json;

namespace JsonProperty.EFCore.Base.Serializers
{
    internal class StringJsonListPropertySerializer<T> :
        AbstractStringJsonPropertySerializer, IJsonListSerializer<T>
    {
        public StringJsonListPropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public IList<T> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrWhiteSpace(prop))
            {
                List<T>? resList = null;

                var res = JsonSerializer.Deserialize<IList<object[]>>(prop);
                if (res is not null)
                {
                    int count = res.Count();
                    T[] arr = new T[count];
                    for (int i = 0; i < count; i++)
                    {
                        object? obj = TypePacker.Unpack(res.ElementAt(i));
                        arr[i] = (T)(obj ?? default!);
                    }
                    resList = arr.ToList();
                }

                if (resList is not null)
                    return resList;
            }
            return new T[0].ToList();
        }

        public void Serialize(IList<T> items)
        {
            string? resString = null;

            int count = items.Count();
            List<object[]> list = new List<object[]>(count);
            for (int i = 0; i < count; i++)
            {
                object[] valueType = TypePacker.Pack(items.ElementAt(i));
                list.Insert(i, valueType);
            }
            resString = JsonSerializer.Serialize(list);

            string res = resString ??
                  throw new NullReferenceException($"{nameof(IJsonListSerializer<T>.Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonListSerializer<T>.Serialize)}");
            SetProp(res);
        }
    }
}
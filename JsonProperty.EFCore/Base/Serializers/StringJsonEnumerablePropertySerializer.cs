using JsonProperty.EFCore.Base.Interfaces.Serializers;
using JsonProperty.EFCore.Base.JsonTyped;
using JsonProperty.EFCore.Base.Serializers.Base;
using Newtonsoft.Json;

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
                List<T>? resList = null;

                var res = JsonConvert.DeserializeObject<IEnumerable<object[]>>(prop);
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

        public void Serialize(IEnumerable<T> items)
        {
            string? resString = null;

            int count = items.Count();
            List<object[]> list = new List<object[]>(count);
            for (int i = 0; i < count; i++)
            {
                object[] valueType = TypePacker.Pack(items.ElementAt(i));
                list.Insert(i, valueType);
            }
            resString = JsonConvert.SerializeObject(list);

            string res = resString ??
                  throw new NullReferenceException($"{nameof(IJsonEnumerableSerializer<T>.Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonEnumerableSerializer<T>.Serialize)}");
            SetProp(res);
        }
    }
}
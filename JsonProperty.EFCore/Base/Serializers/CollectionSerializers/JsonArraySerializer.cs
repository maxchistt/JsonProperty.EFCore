using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using JsonProperty.EFCore.Base.Serializers.CollectionSerializers.TypedJson;
using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializers.CollectionSerializers
{
    internal class JsonArraySerializer<T> : IJsonArraySerializer<T>
    {
        public IList<T>? Deserialize(string? json)
        {
            IList<T>? resList = null;
            var res = JsonConvert.DeserializeObject<IEnumerable<object[]>>(json);
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
            return resList;
        }

        public string Serialize(IEnumerable<T> items)
        {
            int count = items.Count();
            List<object[]> list = new List<object[]>(count);
            for (int i = 0; i < count; i++)
            {
                object[] valueType = TypePacker.Pack(items.ElementAt(i));
                list.Insert(i, valueType);
            }
            return JsonConvert.SerializeObject(list);
        }
    }
}
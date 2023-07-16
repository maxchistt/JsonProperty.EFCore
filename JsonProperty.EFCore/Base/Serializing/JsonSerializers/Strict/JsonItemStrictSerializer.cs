using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using JsonProperty.EFCore.Base.Serializing.JsonSerializers.Strict.TypedJson;
using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializing.JsonSerializers.Strict
{
    internal class JsonItemStrictSerializer<T> : IJsonItemSerializer<T>
    {
        public string Serialize(T item)
        {
            object[] valueType = TypePacker.Pack(item);

            return JsonConvert.SerializeObject(valueType);
        }

        T? IJsonItemSerializer<T>.Deserialize(string? json)
        {
            T? resItem = default;
            var res = JsonConvert.DeserializeObject<object[]>(json ?? JsonConvert.SerializeObject(new object[2]));
            if (res is not null)
            {
                object? obj = TypePacker.Unpack(res);
                resItem = (T)(obj ?? default!);
            }
            return resItem;
        }
    }
}
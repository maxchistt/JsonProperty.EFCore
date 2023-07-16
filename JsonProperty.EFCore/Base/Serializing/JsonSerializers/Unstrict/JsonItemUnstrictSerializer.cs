using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializing.JsonSerializers.Unstrict
{
    internal class JsonItemUnstrictSerializer<T> : IJsonItemSerializer<T>
    {
        public string Serialize(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        T? IJsonItemSerializer<T>.Deserialize(string? json)
        {
            return JsonConvert.DeserializeObject<T>(json ?? JsonConvert.SerializeObject(default(T)!));
        }
    }
}
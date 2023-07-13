using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializers.CollectionSerializers.Unstrict
{
    internal class JsonArrayUnstrictSerializer<T> : IJsonArraySerializer<T>
    {
        public IList<T>? Deserialize(string? json)
        {
            return JsonConvert.DeserializeObject<IList<T>>(json);
        }

        public string Serialize(IEnumerable<T> items)
        {
            return JsonConvert.SerializeObject(items);
        }
    }
}
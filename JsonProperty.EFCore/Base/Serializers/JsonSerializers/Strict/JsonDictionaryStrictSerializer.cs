using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using JsonProperty.EFCore.Base.Serializers.JsonSerializers.Strict.TypedJson;
using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializers.JsonSerializers.Strict
{
    internal class JsonDictionaryStrictSerializer<TKey, TValue> : IJsonDictionarySerializer<TKey, TValue> where TKey : notnull
    {
        public string Serialize(IDictionary<TKey, TValue> items)
        {
            Dictionary<TKey, object[]> dict = new();
            foreach (var item in items)
            {
                object[] valueType = TypePacker.Pack(item.Value);
                dict.Add(item.Key, valueType);
            }
            return JsonConvert.SerializeObject(dict);
        }

        public IDictionary<TKey, TValue>? Deserialize(string? prop)
        {
            IDictionary<TKey, TValue>? resDict = null;
            var res = JsonConvert.DeserializeObject<IDictionary<TKey, object[]>>(prop ?? JsonConvert.SerializeObject(new Dictionary<TKey, object[]>()));
            if (res is not null)
            {
                Dictionary<TKey, TValue> dict = new();
                foreach (var item in res)
                {
                    object? obj = TypePacker.Unpack(item.Value);
                    TValue val = (TValue)(obj ?? default!);
                    dict.Add(item.Key, val);
                }
                resDict = dict;
            }
            return resDict;
        }
    }
}
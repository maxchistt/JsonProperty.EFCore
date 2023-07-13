using JsonProperty.EFCore.Base.Serializers.CollectionSerializers.TypedJson;
using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializers.CollectionSerializers
{
    internal static class DictionarySerializer
    {
        public static string SerializeItems<TKey, TValue>(IDictionary<TKey, TValue> items)
        {
            Dictionary<TKey, object[]> dict = new();
            foreach (var item in items)
            {
                object[] valueType = TypePacker.Pack(item.Value);
                dict.Add(item.Key, valueType);
            }
            return JsonConvert.SerializeObject(dict);
        }

        public static IDictionary<TKey, TValue>? DeserializeItems<TKey, TValue>(string? prop)
        {
            IDictionary<TKey, TValue>? resDict = null;
            var res = JsonConvert.DeserializeObject<IDictionary<TKey, object[]>>(prop);
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
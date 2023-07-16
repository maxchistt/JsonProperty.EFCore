﻿using JsonProperty.EFCore.Base.Interfaces.JsonSerializers;
using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base.Serializing.JsonSerializers.Unstrict
{
    internal class JsonDictionaryUnstrictSerializer<TKey, TValue> : IJsonDictionarySerializer<TKey, TValue> where TKey : notnull
    {
        public string Serialize(IDictionary<TKey, TValue> items)
        {
            return JsonConvert.SerializeObject(items);
        }

        public IDictionary<TKey, TValue>? Deserialize(string? prop)
        {
            return JsonConvert.DeserializeObject<IDictionary<TKey, TValue>>(prop ?? JsonConvert.SerializeObject(new Dictionary<TKey, TValue>()));
        }
    }
}
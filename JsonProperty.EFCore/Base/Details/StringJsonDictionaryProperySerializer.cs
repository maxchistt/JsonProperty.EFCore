﻿using JsonProperty.EFCore.Base.Details.Interfaces;
using System.Text.Json;

namespace JsonProperty.EFCore.Base.Details
{
    internal class StringJsonDictionaryPropertySerializer<TKey, TValue> :
        AbstractStringJsonPropertySerializer, IJsonDictionarySerialize<TKey, TValue>
        where TKey : notnull
    {
        public StringJsonDictionaryPropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public void Serialize(IDictionary<TKey, TValue> items)
        {
            string res = JsonSerializer.Serialize(items) ??
                throw new NullReferenceException($"{nameof(IJsonDictionarySerialize<TKey, TValue>.Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonDictionarySerialize<TKey, TValue>.Serialize)}");
            SetProp(res);
        }

        public IDictionary<TKey, TValue> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrEmpty(prop))
            {
                var res = JsonSerializer.Deserialize<IDictionary<TKey, TValue>>(prop);
                if (res is not null) return res;
            }
            return new Dictionary<TKey, TValue>();
        }
    }
}
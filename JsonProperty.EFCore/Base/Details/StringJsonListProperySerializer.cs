﻿using JsonProperty.EFCore.Base.Details.Interfaces;
using System.Text.Json;

namespace JsonProperty.EFCore.Base.Details
{
    internal class StringJsonListPropertySerializer<T> :
        AbstractStringJsonPropertySerializer, IJsonListSerialize<T>
    {
        public StringJsonListPropertySerializer(object parent, string? propName) : base(parent, propName)
        {
        }

        public IList<T> Deserialize()
        {
            var prop = GetProp();
            if (!string.IsNullOrWhiteSpace(prop))
            {
                var res = JsonSerializer.Deserialize<IList<T>>(prop);
                if (res is not null) return res;
            }
            return new T[0];
        }

        public void Serialize(IList<T> items)
        {
            string res = JsonSerializer.Serialize(items) ??
                  throw new NullReferenceException($"{nameof(IJsonListSerialize<T>.Serialize)} set null fail");
            if (string.IsNullOrEmpty(res))
                throw new ArgumentException($"Empty string to set in {nameof(IJsonListSerialize<T>.Serialize)}");
            SetProp(res);
        }
    }
}
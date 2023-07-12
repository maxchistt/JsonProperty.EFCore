﻿namespace JsonPropertyAdapter.Base.Details.Interfaces
{
    public interface IJsonDictionarySerialize<TKey, TValue>
    {
        public IDictionary<TKey, TValue> JsonDictionaryDeserialize();

        public void JsonDictionarySerialize(IDictionary<TKey, TValue> items);
    }
}
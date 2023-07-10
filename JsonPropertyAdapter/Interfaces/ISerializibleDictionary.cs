﻿using JsonPropertyAdapter.Details.Interfaces;

namespace JsonPropertyAdapter.Interfaces
{
    public interface ISerializibleDictionary<TKey, TValue> : IJsonDictionarySerialize<TKey, TValue>, IEditableDictionary<TKey, TValue>
    {
    }
}
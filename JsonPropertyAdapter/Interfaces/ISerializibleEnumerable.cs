﻿using JsonPropertyAdapter.Details.Interfaces;

namespace JsonPropertyAdapter.Interfaces
{
    public interface ISerializibleEnumerable<T> : IJsonEnumerableSerialize<T>, IEditableEnumerable<T>
    {
        public IEnumerable<T> VirtualEnumerable { get; set; }
    }
}
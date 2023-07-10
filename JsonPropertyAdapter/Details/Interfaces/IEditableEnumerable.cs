﻿namespace JsonPropertyAdapter.Details.Interfaces
{
    public interface IEditableEnumerable<T>
    {
        public void Edit(Func<IEnumerable<T>, IEnumerable<T>> EditingAction);

        public void Edit(Action<IEnumerable<T>> EditingAction);

        public void AddRange(IEnumerable<T> items);

        public void Add(T item);
    }
}
namespace JsonProperty.EFCore.Base.Interfaces.Editable
{
    public interface IEditableList<T> : IEditableEnumerable<T>
    {
        public void Edit(Func<IList<T>, IList<T>> EditingAction);

        public void Edit(Func<IList<T>, IEnumerable<T>> EditingAction);

        public void AddRange(IList<T> items);
    }
}
namespace JsonProperty.EFCore.Base.Interfaces.Editable
{
    public interface IEditableItem<T>
    {
        public void Edit(Func<T, T> EditingAction);
    }
}
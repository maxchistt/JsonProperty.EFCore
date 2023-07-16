using JsonProperty.EFCore.Base.Interfaces.Editable;
using JsonProperty.EFCore.Base.Interfaces.Serializible;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleItemContainer<T> : ISerializibleItem<T>, IEditableItem<T>
    {
        public T VirtualItem { get; set; }
    }
}
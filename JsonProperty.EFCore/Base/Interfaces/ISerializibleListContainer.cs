using JsonProperty.EFCore.Base.Interfaces.Editable;
using JsonProperty.EFCore.Base.Interfaces.Serializible;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleListContainer<T> : ISerializibleList<T>, IEditableList<T>
    {
        public IList<T> VirtualList { get; set; }
    }
}
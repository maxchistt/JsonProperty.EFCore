using JsonProperty.EFCore.Base.Interfaces.Editable;
using JsonProperty.EFCore.Base.Interfaces.Serializible;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleArrayContainer<T> : ISerializibleArray<T>, IEditableArray<T>
    {
        public IList<T> VirtualList { get; set; }
    }
}
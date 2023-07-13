using JsonProperty.EFCore.Base.Details.Interfaces.Serialize;
using JsonProperty.EFCore.Base.Interfaces.Editable;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleList<T> : IJsonListSerializer<T>, IEditableList<T>
    {
        public IList<T> VirtualList { get; set; }
    }
}
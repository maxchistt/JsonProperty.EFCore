using JsonProperty.EFCore.Base.Interfaces.Editable;
using JsonProperty.EFCore.Base.Interfaces.Serializers;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleList<T> : IJsonListSerializer<T>, IEditableList<T>
    {
        public IList<T> VirtualList { get; set; }
    }
}
using JsonProperty.EFCore.Base.Interfaces.Editable;
using JsonProperty.EFCore.Base.Interfaces.Serializible;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleEnumerableContainer<T> : ISerializibleEnumerable<T>, IEditableEnumerable<T>
    {
        public IEnumerable<T> VirtualEnumerable { get; set; }
    }
}
using JsonProperty.EFCore.Base.Details.Interfaces.Serialize;
using JsonProperty.EFCore.Base.Interfaces.Editable;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleEnumerable<T> : IJsonEnumerableSerializer<T>, IEditableEnumerable<T>
    {
        public IEnumerable<T> VirtualEnumerable { get; set; }
    }
}
using JsonProperty.EFCore.Base.Interfaces.Editable;
using JsonProperty.EFCore.Base.Interfaces.Serializers;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleEnumerable<T> : IJsonEnumerableSerializer<T>, IEditableEnumerable<T>
    {
        public IEnumerable<T> VirtualEnumerable { get; set; }
    }
}
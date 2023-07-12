using JsonProperty.EFCore.Base.Details.Interfaces;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleEnumerable<T> : IJsonEnumerableSerialize<T>, IEditableEnumerable<T>
    {
        public IEnumerable<T> VirtualEnumerable { get; set; }
    }
}
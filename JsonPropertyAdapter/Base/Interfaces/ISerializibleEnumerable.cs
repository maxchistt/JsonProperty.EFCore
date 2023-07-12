using JsonPropertyAdapter.Base.Details.Interfaces;

namespace JsonPropertyAdapter.Base.Interfaces
{
    public interface ISerializibleEnumerable<T> : IJsonEnumerableSerialize<T>, IEditableEnumerable<T>
    {
        public IEnumerable<T> VirtualEnumerable { get; set; }
    }
}
using JsonPropertyAdapter.Base.Details.Interfaces;

namespace JsonPropertyAdapter.Base.Interfaces
{
    public interface ISerializibleList<T> : IJsonListSerialize<T>, IEditableList<T>
    {
        public IList<T> VirtualList { get; set; }
    }
}
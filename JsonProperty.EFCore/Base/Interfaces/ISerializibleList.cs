using JsonProperty.EFCore.Base.Details.Interfaces;

namespace JsonProperty.EFCore.Base.Interfaces
{
    public interface ISerializibleList<T> : IJsonListSerialize<T>, IEditableList<T>
    {
        public IList<T> VirtualList { get; set; }
    }
}
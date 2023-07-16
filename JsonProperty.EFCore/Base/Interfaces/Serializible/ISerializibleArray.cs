namespace JsonProperty.EFCore.Base.Interfaces.Serializible
{
    public interface ISerializibleArray<T> : ISerializibleEnumerable<T>, ISerializibleList<T>
    {
        public new IList<T> Deserialize();

        public new void Serialize(IEnumerable<T> items);
    }
}
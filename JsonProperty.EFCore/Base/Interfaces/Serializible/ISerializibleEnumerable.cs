namespace JsonProperty.EFCore.Base.Interfaces.Serializible
{
    public interface ISerializibleEnumerable<T>
    {
        public IEnumerable<T> Deserialize();

        public void Serialize(IEnumerable<T> items);
    }
}
namespace JsonProperty.EFCore.Base.Interfaces.Serializible
{
    public interface ISerializibleList<T>
    {
        public IList<T> Deserialize();

        public void Serialize(IList<T> items);
    }
}
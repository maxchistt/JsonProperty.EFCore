namespace JsonProperty.EFCore.Base.Interfaces.Serializible
{
    public interface ISerializibleItem<T>
    {
        public T Deserialize();

        public void Serialize(T item);
    }
}
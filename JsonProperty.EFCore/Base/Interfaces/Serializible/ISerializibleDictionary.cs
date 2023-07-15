namespace JsonProperty.EFCore.Base.Interfaces.Serializible
{
    public interface ISerializibleDictionary<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Deserialize();

        public void Serialize(IDictionary<TKey, TValue> items);
    }
}
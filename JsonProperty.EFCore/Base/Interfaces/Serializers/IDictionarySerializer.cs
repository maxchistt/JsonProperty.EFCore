namespace JsonProperty.EFCore.Base.Interfaces.Serializers
{
    public interface IDictionarySerializer<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Deserialize();

        public void Serialize(IDictionary<TKey, TValue> items);
    }
}
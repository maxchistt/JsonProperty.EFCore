namespace JsonProperty.EFCore.Base.Details.Interfaces.Serialize
{
    public interface IJsonDictionarySerializer<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Deserialize();

        public void Serialize(IDictionary<TKey, TValue> items);
    }
}
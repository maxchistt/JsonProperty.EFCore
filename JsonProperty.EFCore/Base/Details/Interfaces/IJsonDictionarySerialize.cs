namespace JsonProperty.EFCore.Base.Details.Interfaces
{
    public interface IJsonDictionarySerialize<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Deserialize();

        public void Serialize(IDictionary<TKey, TValue> items);
    }
}
namespace JsonProperty.EFCore.Base.Interfaces.JsonSerializers
{
    internal interface IJsonDictionarySerializer<TKey, TValue>
    {
        IDictionary<TKey, TValue>? Deserialize(string? prop);

        string Serialize(IDictionary<TKey, TValue> items);
    }
}
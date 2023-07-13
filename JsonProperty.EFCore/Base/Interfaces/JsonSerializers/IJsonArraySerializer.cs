namespace JsonProperty.EFCore.Base.Interfaces.JsonSerializers
{
    internal interface IJsonArraySerializer<T>
    {
        IList<T>? Deserialize(string? json);

        string Serialize(IEnumerable<T> items);
    }
}
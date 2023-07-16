namespace JsonProperty.EFCore.Base.Interfaces.JsonSerializers
{
    internal interface IJsonItemSerializer<T>
    {
        T? Deserialize(string? json);

        string Serialize(T item);
    }
}
namespace JsonProperty.EFCore.Tests.Shared
{
    public class Product
    {
        public string? Name { get; set; }

        public JsonDictionary Parameters { get; set; } = new();
    }
}
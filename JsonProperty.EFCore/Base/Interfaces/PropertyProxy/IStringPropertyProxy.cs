namespace JsonProperty.EFCore.Base.Interfaces.PropertyProxy
{
    internal interface IStringPropertyProxy
    {
        public string? Get();

        public void Set(string value);
    }
}
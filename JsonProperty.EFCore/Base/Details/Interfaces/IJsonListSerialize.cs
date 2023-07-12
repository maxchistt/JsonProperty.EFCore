namespace JsonProperty.EFCore.Base.Details.Interfaces
{
    public interface IJsonListSerialize<T>
    {
        public IList<T> JsonListDeserialize();

        public void JsonListSerialize(IList<T> items);
    }
}
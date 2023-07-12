namespace JsonProperty.EFCore.Base.Details.Interfaces
{
    public interface IJsonListSerialize<T>
    {
        public IList<T> Deserialize();

        public void Serialize(IList<T> items);
    }
}
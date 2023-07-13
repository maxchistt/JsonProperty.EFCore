namespace JsonProperty.EFCore.Base.Details.Interfaces.Serialize
{
    public interface IJsonListSerializer<T>
    {
        public IList<T> Deserialize();

        public void Serialize(IList<T> items);
    }
}
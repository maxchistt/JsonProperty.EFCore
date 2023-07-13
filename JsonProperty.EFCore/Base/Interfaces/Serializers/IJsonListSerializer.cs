namespace JsonProperty.EFCore.Base.Interfaces.Serializers
{
    public interface IJsonListSerializer<T>
    {
        public IList<T> Deserialize();

        public void Serialize(IList<T> items);
    }
}
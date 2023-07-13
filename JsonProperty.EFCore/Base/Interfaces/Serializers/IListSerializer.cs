namespace JsonProperty.EFCore.Base.Interfaces.Serializers
{
    public interface IListSerializer<T>
    {
        public IList<T> Deserialize();

        public void Serialize(IList<T> items);
    }
}
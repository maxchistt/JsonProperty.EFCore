namespace JsonProperty.EFCore.Base.Interfaces.Serializers
{
    public interface IEnumerableSerializer<T>
    {
        public IEnumerable<T> Deserialize();

        public void Serialize(IEnumerable<T> items);
    }
}
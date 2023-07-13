namespace JsonProperty.EFCore.Base.Details.Interfaces.Serialize
{
    public interface IJsonEnumerableSerializer<T>
    {
        public IEnumerable<T> Deserialize();

        public void Serialize(IEnumerable<T> items);
    }
}
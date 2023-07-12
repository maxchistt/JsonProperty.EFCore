namespace JsonProperty.EFCore.Base.Details.Interfaces
{
    public interface IJsonEnumerableSerialize<T>
    {
        public IEnumerable<T> Deserialize();

        public void Serialize(IEnumerable<T> items);
    }
}
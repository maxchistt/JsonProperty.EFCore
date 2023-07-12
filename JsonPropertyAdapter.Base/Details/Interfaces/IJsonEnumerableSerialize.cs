namespace JsonPropertyAdapter.Base.Details.Interfaces
{
    public interface IJsonEnumerableSerialize<T>
    {
        public IEnumerable<T> JsonEnumerableDeserialize();

        public void JsonEnumerableSerialize(IEnumerable<T> items);
    }
}
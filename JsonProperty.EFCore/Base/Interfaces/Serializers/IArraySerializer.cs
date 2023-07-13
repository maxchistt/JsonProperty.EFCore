namespace JsonProperty.EFCore.Base.Interfaces.Serializers
{
    public interface IArraySerializer<T> : IEnumerableSerializer<T>, IListSerializer<T>
    {
    }
}
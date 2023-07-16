using JsonProperty.EFCore.Base.Interfaces;
using JsonProperty.EFCore.Base.Interfaces.Serializible;
using JsonProperty.EFCore.Base.Serializing;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore.Base
{
    public abstract class JsonItemAdaptableContainer<T> : ISerializibleItemContainer<T>
    {
        private ISerializibleItem<T> JsonSerializing { get; }

        [NotMapped, JsonIgnore]
        public T VirtualItem { get => Deserialize(); set => Serialize(value); }

        protected JsonItemAdaptableContainer(string? manualPropNameSet)
        {
            JsonSerializing = new JsonItemStringPropertySerializible<T>(this, manualPropNameSet);
        }

        protected JsonItemAdaptableContainer() : this(null)
        {
        }

        public T Deserialize()
        {
            return JsonSerializing.Deserialize();
        }

        public void Serialize(T item)
        {
            JsonSerializing.Serialize(item);
        }

        public void Edit(Func<T, T> EditingAction)
        {
            var res = EditingAction.Invoke(Deserialize());
            Serialize(res);
        }
    }
}
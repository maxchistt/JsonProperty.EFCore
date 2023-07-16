using JsonProperty.EFCore.Base.Interfaces;
using JsonProperty.EFCore.Base.Interfaces.Serializible;
using JsonProperty.EFCore.Base.Serializing;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore.Base
{
    public abstract class JsonEnumerableAdaptableContainer<T_ListItem> : ISerializibleEnumerableContainer<T_ListItem>
    {
        [NotMapped]
        private ISerializibleEnumerable<T_ListItem> JsonSerializing { get; }

        [NotMapped]
        public IEnumerable<T_ListItem> VirtualEnumerable { get => Deserialize(); set => Serialize(value); }

        protected JsonEnumerableAdaptableContainer(string? manualPropNameSet)
        {
            JsonSerializing = new JsonArrayStringPropertySerializible<T_ListItem>(this, manualPropNameSet);
        }

        protected JsonEnumerableAdaptableContainer() : this(null)
        {
        }

        public void AddRange(IEnumerable<T_ListItem> items)
        {
            Edit(en => en.Concat(items));
        }

        public void Add(T_ListItem item)
        {
            Edit(en => en.Append(item));
        }

        public void Edit(Func<IEnumerable<T_ListItem>, IEnumerable<T_ListItem>> EditingAction)
        {
            var l = JsonSerializing.Deserialize();
            IEnumerable<T_ListItem> res = EditingAction.Invoke(l);
            JsonSerializing.Serialize(res);
        }

        public IEnumerable<T_ListItem> Deserialize()
        {
            return JsonSerializing.Deserialize();
        }

        public void Serialize(IEnumerable<T_ListItem> items)
        {
            JsonSerializing.Serialize(items);
        }
    }
}
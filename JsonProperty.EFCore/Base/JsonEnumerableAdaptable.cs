using JsonProperty.EFCore.Base.Details;
using JsonProperty.EFCore.Base.Details.Interfaces;
using JsonProperty.EFCore.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore.Base
{
    public abstract class JsonEnumerableAdaptable<T_ListItem> : ISerializibleEnumerable<T_ListItem>
    {
        [NotMapped]
        private IJsonEnumerableSerialize<T_ListItem> JsonSerializing { get; }

        [NotMapped]
        public IEnumerable<T_ListItem> VirtualEnumerable { get => JsonEnumerableDeserialize(); set => JsonEnumerableSerialize(value); }

        protected JsonEnumerableAdaptable(string? manualPropNameSet)
        {
            JsonSerializing = new StringJsonEnumerablePropertySerializer<T_ListItem>(this, manualPropNameSet);
        }

        protected JsonEnumerableAdaptable() : this(null)
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
            IEnumerable<T_ListItem> res = EditingAction.Invoke(JsonSerializing.JsonEnumerableDeserialize());
            JsonSerializing.JsonEnumerableSerialize(res);
        }

        public IEnumerable<T_ListItem> JsonEnumerableDeserialize()
        {
            return JsonSerializing.JsonEnumerableDeserialize();
        }

        public void JsonEnumerableSerialize(IEnumerable<T_ListItem> items)
        {
            JsonSerializing.JsonEnumerableSerialize(items);
        }
    }
}
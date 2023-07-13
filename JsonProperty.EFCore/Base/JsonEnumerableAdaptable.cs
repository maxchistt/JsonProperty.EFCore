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
        public IEnumerable<T_ListItem> VirtualEnumerable { get => Deserialize(); set => Serialize(value); }

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
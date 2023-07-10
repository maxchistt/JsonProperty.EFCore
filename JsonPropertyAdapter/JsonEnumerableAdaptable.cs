using JsonPropertyAdapter.Details;
using JsonPropertyAdapter.Details.Interfaces;
using JsonPropertyAdapter.Interfaces;

namespace JsonPropertyAdapter
{
    public abstract class JsonEnumerableAdaptable<T_ListItem> : ISerializibleEnumerable<T_ListItem>
    {
        private IJsonEnumerableSerialize<T_ListItem> JsonSerializing { get; }

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

        public void Edit(Action<IEnumerable<T_ListItem>> EditingAction)
        {
            IEnumerable<T_ListItem> enumerable = JsonSerializing.JsonEnumerableDeserialize();
            EditingAction.Invoke(enumerable);
            JsonSerializing.JsonEnumerableSerialize(enumerable);
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
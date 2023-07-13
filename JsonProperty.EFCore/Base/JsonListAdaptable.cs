using JsonProperty.EFCore.Base.Interfaces;
using JsonProperty.EFCore.Base.Interfaces.Serializers;
using JsonProperty.EFCore.Base.Serializers;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore.Base
{
    public abstract class JsonListAdaptable<T_ListItem> : ISerializibleList<T_ListItem>
    {
        [NotMapped]
        private IJsonListSerializer<T_ListItem> JsonSerializing { get; }

        [NotMapped]
        public IList<T_ListItem> VirtualList { get => Deserialize(); set => Serialize(value); }

        protected JsonListAdaptable(string? manualPropNameSet)
        {
            JsonSerializing = new StringJsonArrayPropertySerializer<T_ListItem>(this, manualPropNameSet);
        }

        protected JsonListAdaptable() : this(null)
        {
        }

        public void AddRange(IList<T_ListItem> items)
        {
            Edit(en =>
            {
                var list = en.ToList();
                list.AddRange(items);
                return list;
            });
        }

        public void Add(T_ListItem item)
        {
            Edit(en =>
            {
                var list = en.ToList();
                list.Add(item);
                return list;
            });
        }

        public void Edit(Func<IList<T_ListItem>, IList<T_ListItem>> EditingAction)
        {
            JsonSerializing.Serialize(EditingAction.Invoke(JsonSerializing.Deserialize()));
        }

        public IList<T_ListItem> Deserialize()
        {
            return JsonSerializing.Deserialize();
        }

        public void Serialize(IList<T_ListItem> items)
        {
            JsonSerializing.Serialize(items);
        }

        public void Edit(Func<IList<T_ListItem>, IEnumerable<T_ListItem>> EditingAction)
        {
            JsonSerializing.Serialize(EditingAction.Invoke(JsonSerializing.Deserialize()).ToList());
        }

        public void Edit(Func<IEnumerable<T_ListItem>, IEnumerable<T_ListItem>> EditingAction)
        {
            JsonSerializing.Serialize(EditingAction.Invoke(JsonSerializing.Deserialize()).ToList());
        }

        public void AddRange(IEnumerable<T_ListItem> items)
        {
            Edit(en =>
            {
                var list = en.ToList();
                list.AddRange(items);
                return list;
            });
        }
    }
}
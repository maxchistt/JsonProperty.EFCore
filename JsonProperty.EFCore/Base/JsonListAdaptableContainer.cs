using JsonProperty.EFCore.Base.Interfaces;
using JsonProperty.EFCore.Base.Interfaces.Serializible;
using JsonProperty.EFCore.Base.Serializing;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace JsonProperty.EFCore.Base
{
    public abstract class JsonListAdaptableContainer<T_ListItem> : ISerializibleListContainer<T_ListItem>
    {
        private ISerializibleList<T_ListItem> JsonSerializing { get; }

        [NotMapped, JsonIgnore]
        public IList<T_ListItem> VirtualList { get => Deserialize(); set => Serialize(value); }

        protected JsonListAdaptableContainer(string? manualPropNameSet)
        {
            JsonSerializing = new JsonArrayStringPropertySerializible<T_ListItem>(this, manualPropNameSet);
        }

        protected JsonListAdaptableContainer() : this(null)
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
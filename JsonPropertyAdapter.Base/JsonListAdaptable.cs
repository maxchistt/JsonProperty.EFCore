﻿using JsonPropertyAdapter.Base.Details;
using JsonPropertyAdapter.Base.Details.Interfaces;
using JsonPropertyAdapter.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonPropertyAdapter.Base
{
    public abstract class JsonListAdaptable<T_ListItem> : ISerializibleList<T_ListItem>
    {
        [NotMapped]
        private IJsonListSerialize<T_ListItem> JsonSerializing { get; }

        [NotMapped]
        public IList<T_ListItem> VirtualList { get => JsonListDeserialize(); set => JsonListSerialize(value); }

        protected JsonListAdaptable(string? manualPropNameSet)
        {
            JsonSerializing = new StringJsonListPropertySerializer<T_ListItem>(this, manualPropNameSet);
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
            JsonSerializing.JsonListSerialize(EditingAction.Invoke(JsonSerializing.JsonListDeserialize()));
        }

        public IList<T_ListItem> JsonListDeserialize()
        {
            return JsonSerializing.JsonListDeserialize();
        }

        public void JsonListSerialize(IList<T_ListItem> items)
        {
            JsonSerializing.JsonListSerialize(items);
        }

        public void Edit(Func<IList<T_ListItem>, IEnumerable<T_ListItem>> EditingAction)
        {
            JsonSerializing.JsonListSerialize(EditingAction.Invoke(JsonSerializing.JsonListDeserialize()).ToList());
        }

        public void Edit(Func<IEnumerable<T_ListItem>, IEnumerable<T_ListItem>> EditingAction)
        {
            JsonSerializing.JsonListSerialize(EditingAction.Invoke(JsonSerializing.JsonListDeserialize()).ToList());
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
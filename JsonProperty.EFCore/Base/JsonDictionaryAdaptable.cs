﻿using JsonProperty.EFCore.Base.Interfaces;
using JsonProperty.EFCore.Base.Interfaces.Serializers;
using JsonProperty.EFCore.Base.Serializers;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore.Base
{
    public abstract class JsonDictionaryAdaptable<TKey, TValue> : ISerializibleDictionary<TKey, TValue> where TKey : notnull
    {
        [NotMapped]
        private IDictionarySerializer<TKey, TValue> JsonSerializing { get; }

        [NotMapped]
        public IDictionary<TKey, TValue> VirtualDictionary { get => Deserialize(); set => Serialize(value); }

        protected JsonDictionaryAdaptable(string? manualPropNameSet)
        {
            JsonSerializing = new JsonDictionaryStringPropertySerializer<TKey, TValue>(this, manualPropNameSet);
        }

        protected JsonDictionaryAdaptable() : this(null)
        {
        }

        public IDictionary<TKey, TValue> Deserialize()
        {
            return JsonSerializing.Deserialize();
        }

        public void Serialize(IDictionary<TKey, TValue> items)
        {
            JsonSerializing.Serialize(items);
        }

        public void Edit(Func<IDictionary<TKey, TValue>, IDictionary<TKey, TValue>> EditingAction)
        {
            JsonSerializing.Serialize(EditingAction.Invoke(JsonSerializing.Deserialize()));
        }

        public void AddRange(IDictionary<TKey, TValue> items)
        {
            Edit(en =>
            {
                items.ToList().ForEach(x => en.Add(x.Key, x.Value));
                return en;
            });
        }

        public void Add(TKey key, TValue item)
        {
            Edit(en =>
            {
                en.Add(key, item);
                return en;
            });
        }

        public void Edit(Func<IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>> EditingAction)
        {
            Dictionary<TKey, TValue> dict = new();
            var res = EditingAction.Invoke(JsonSerializing.Deserialize());
            res.ToList().ForEach(x => dict.Add(x.Key, x.Value));
            JsonSerializing.Serialize(dict);
        }

        public void Edit(Func<IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>> EditingAction)
        {
            Dictionary<TKey, TValue> dict = new();
            var res = EditingAction.Invoke(JsonSerializing.Deserialize());
            res.ToList().ForEach(x => dict.Add(x.Key, x.Value));
            JsonSerializing.Serialize(dict);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            Edit(en =>
            {
                items.ToList().ForEach(x => en.Add(x.Key, x.Value));
                return en;
            });
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Edit(en =>
            {
                en.Add(item);
                return en;
            });
        }
    }
}
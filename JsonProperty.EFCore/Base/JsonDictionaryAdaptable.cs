using JsonProperty.EFCore.Base.Details;
using JsonProperty.EFCore.Base.Details.Interfaces;
using JsonProperty.EFCore.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore.Base
{
    public abstract class JsonDictionaryAdaptable<TKey, TValue> : ISerializibleDictionary<TKey, TValue> where TKey : notnull
    {
        [NotMapped]
        private IJsonDictionarySerialize<TKey, TValue> JsonSerializing { get; }

        [NotMapped]
        public IDictionary<TKey, TValue> VirtualDictionary { get => JsonDictionaryDeserialize(); set => JsonDictionarySerialize(value); }

        protected JsonDictionaryAdaptable(string? manualPropNameSet)
        {
            JsonSerializing = new StringJsonDictionaryPropertySerializer<TKey, TValue>(this, manualPropNameSet);
        }

        protected JsonDictionaryAdaptable() : this(null)
        {
        }

        public IDictionary<TKey, TValue> JsonDictionaryDeserialize()
        {
            return JsonSerializing.JsonDictionaryDeserialize();
        }

        public void JsonDictionarySerialize(IDictionary<TKey, TValue> items)
        {
            JsonSerializing.JsonDictionarySerialize(items);
        }

        public void Edit(Func<IDictionary<TKey, TValue>, IDictionary<TKey, TValue>> EditingAction)
        {
            JsonSerializing.JsonDictionarySerialize(EditingAction.Invoke(JsonSerializing.JsonDictionaryDeserialize()));
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
            var res = EditingAction.Invoke(JsonSerializing.JsonDictionaryDeserialize());
            res.ToList().ForEach(x => dict.Add(x.Key, x.Value));
            JsonSerializing.JsonDictionarySerialize(dict);
        }

        public void Edit(Func<IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>> EditingAction)
        {
            Dictionary<TKey, TValue> dict = new();
            var res = EditingAction.Invoke(JsonSerializing.JsonDictionaryDeserialize());
            res.ToList().ForEach(x => dict.Add(x.Key, x.Value));
            JsonSerializing.JsonDictionarySerialize(dict);
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
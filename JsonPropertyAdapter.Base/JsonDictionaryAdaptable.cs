using JsonPropertyAdapter.Base.Details;
using JsonPropertyAdapter.Base.Details.Interfaces;
using JsonPropertyAdapter.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonPropertyAdapter.Base
{
    public abstract class JsonDictionaryAdaptable<TKey, TValue> : ISerializibleDictionary<TKey, TValue>
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
            IDictionary<TKey, TValue> res = EditingAction.Invoke(JsonSerializing.JsonDictionaryDeserialize());
            JsonSerializing.JsonDictionarySerialize(res);
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
    }
}
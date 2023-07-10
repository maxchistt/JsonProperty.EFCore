using JsonPropertyAdapter.Details;
using JsonPropertyAdapter.Details.Interfaces;
using JsonPropertyAdapter.Interfaces;

namespace JsonPropertyAdapter
{
    public abstract class JsonDictionaryAdaptable<TKey, TValue> : ISerializibleDictionary<TKey, TValue>
    {
        private IJsonDictionarySerialize<TKey, TValue> JsonSerializing { get; }

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

        public void Edit(Action<IDictionary<TKey, TValue>> EditingAction)
        {
            IDictionary<TKey, TValue> enumerable = JsonSerializing.JsonDictionaryDeserialize();
            EditingAction.Invoke(enumerable);
            JsonSerializing.JsonDictionarySerialize(enumerable);
        }

        public void AddRange(IDictionary<TKey, TValue> items)
        {
            Edit(en => en.Concat(items));
        }

        public void Add(TKey key, TValue item)
        {
            Edit(en => en.Add(key, item));
        }
    }
}
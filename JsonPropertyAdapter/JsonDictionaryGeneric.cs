using JsonPropertyAdapter.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonPropertyAdapter
{
    [Owned]
    public class JsonDictionary<TKey, TValue> : JsonDictionaryAdaptable<TKey, TValue> where TKey : notnull
    {
        [Column]
        public string? JsonString { get; set; }

        public JsonDictionary() : base(nameof(JsonString))
        {
        }
    }
}
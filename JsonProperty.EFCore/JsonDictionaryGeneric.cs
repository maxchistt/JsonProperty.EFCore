using JsonProperty.EFCore.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore
{
    [Owned]
    public class JsonDictionary<TKey, TValue> : JsonDictionaryAdaptableContainer<TKey, TValue> where TKey : notnull
    {
        [Column]
        public string? JsonString { get; set; }

        public JsonDictionary() : base(nameof(JsonString))
        {
        }
    }
}
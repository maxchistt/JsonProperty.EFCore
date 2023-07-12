using JsonPropertyAdapter.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonPropertyAdapter.EFCore.Base
{
    [Owned]
    public abstract class JsonDictionaryBase<TKey, TValue> : JsonDictionaryAdaptable<TKey, TValue> where TKey : notnull
    {
        [Column]
        public string? JsonString { get; set; }

        public JsonDictionaryBase() : base(nameof(JsonString))
        {
        }
    }
}
using JsonPropertyAdapter.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonPropertyAdapter.EFCore
{
    [Owned]
    public class JsonEnumerable<T> : JsonEnumerableAdaptable<T>
    {
        [Column]
        public string? JsonString { get; set; }

        public JsonEnumerable() : base(nameof(JsonString))
        {
        }
    }
}
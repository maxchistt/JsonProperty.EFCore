using JsonProperty.EFCore.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore
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
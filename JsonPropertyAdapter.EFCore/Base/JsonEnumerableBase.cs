using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonPropertyAdapter.EFCore.Base
{
    [Owned]
    public abstract class JsonEnumerableBase<T> : JsonEnumerableAdaptable<T>
    {
        [Column]
        public string? JsonString { get; set; }

        public JsonEnumerableBase() : base(nameof(JsonString))
        {
        }
    }
}
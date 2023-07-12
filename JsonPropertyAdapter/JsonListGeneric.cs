using JsonPropertyAdapter.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonPropertyAdapter
{
    [Owned]
    public class JsonList<T> : JsonListAdaptable<T>
    {
        [Column]
        public string? JsonString { get; set; }

        public JsonList() : base(nameof(JsonString))
        {
        }
    }
}
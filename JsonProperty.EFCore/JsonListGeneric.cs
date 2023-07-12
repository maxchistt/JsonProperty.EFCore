using JsonProperty.EFCore.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore
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
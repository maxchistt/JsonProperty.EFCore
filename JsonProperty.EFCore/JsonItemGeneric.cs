using JsonProperty.EFCore.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace JsonProperty.EFCore
{
    [Owned]
    public class JsonItem<T> : JsonItemAdaptableContainer<T>
    {
        [Column]
        public string? JsonString { get; set; }

        public JsonItem() : base(nameof(JsonString))
        {
        }
    }
}
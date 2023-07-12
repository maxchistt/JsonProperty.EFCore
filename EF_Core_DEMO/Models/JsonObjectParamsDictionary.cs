using JsonPropertyAdapter.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Core_DEMO.Models
{
    [Owned]
    public class JsonObjectParamsDictionary : JsonDictionaryAdaptable<string, object>
    {
        [Column("JsonParametersDictionary")]
        public string? JsonString { get; set; }
    }
}
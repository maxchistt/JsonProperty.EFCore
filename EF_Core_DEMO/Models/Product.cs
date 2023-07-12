using JsonPropertyAdapter.EFCore;
using System.ComponentModel.DataAnnotations;

namespace EF_Core_DEMO.Models
{
    public class Product
    {
        [Key, Required]
        public int Id { get; set; }

        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public ushort? Amount { get; set; }
        public JsonDictionary Parameters { get; set; } = new();
    }
}
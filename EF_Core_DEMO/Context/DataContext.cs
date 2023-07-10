using EF_Core_DEMO.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core_DEMO.Context
{
    public class DataContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Product> Goods { get; set; } = null!;

        public DataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={nameof(EF_Core_DEMO)} Sqlite.db");
        }
    }
}
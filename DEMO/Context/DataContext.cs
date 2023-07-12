using DEMO.Models;
using Microsoft.EntityFrameworkCore;

namespace DEMO.Context
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
            optionsBuilder.UseSqlite($"Data Source={nameof(DEMO)} Sqlite.db");
        }
    }
}
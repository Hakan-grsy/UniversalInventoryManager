using Microsoft.EntityFrameworkCore;
using UniversalInventoryManager.Console.Models;

namespace UniversalInventoryManager.Console.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<TechProduct> TechProducts { get; set; }
        public DbSet<ClothingProduct> ClothingProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }
    }
}
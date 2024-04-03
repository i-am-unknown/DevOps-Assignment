
using Microsoft.EntityFrameworkCore;
using SampleApp.Model;

namespace SampleApp.Data
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)

        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Product 1", Quantity = 10, Price = 20.5m },
                new Product { ProductId = 2, ProductName = "Product 2", Quantity = 15, Price = 30.75m },
                new Product { ProductId = 3, ProductName = "Product 3", Quantity = 20, Price = 25.0m },
                new Product { ProductId = 4, ProductName = "Product 4", Quantity = 25, Price = 15.25m },
                new Product { ProductId = 5, ProductName = "Product 5", Quantity = 30, Price = 10.0m }
            );
        }

    }
}

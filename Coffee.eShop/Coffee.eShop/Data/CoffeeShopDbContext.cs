using Coffee.eShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Coffee.eShop.Data;

public class CoffeeShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Americano", Price = 10.01M, Detail = "Coffee Americano", IsTrendingProduct = false, ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
            new Product { Id = 2, Name = "Cortado", Price = 10.01M, Detail = "Coffee Cortado", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
            new Product { Id = 3, Name = "Mocha", Price = 10.01M, Detail = "Coffee Mocha", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" }
            new Product { Id = 4, Name = "Macchiato", Price = 10.01M, Detail = "Coffee Macchiato", IsTrendingProduct = false, ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
            new Product { Id = 5, Name = "Flat White", Price = 10.01M, Detail = "Coffee Flat White", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
            new Product { Id = 6, Name = "Decaf", Price = 10.01M, Detail = "Coffee Decaf", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" }
            new Product { Id = 7, Name = "Irish Coffee", Price = 10.01M, Detail = "Irish Coffee", IsTrendingProduct = false, ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
            new Product { Id = 8, Name = "Iced Coffee", Price = 10.01M, Detail = "Iced Coffee", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
            new Product { Id = 9, Name = "Latte Mocha", Price = 10.01M, Detail = "Latte Mocha", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" }
        );
    }
}

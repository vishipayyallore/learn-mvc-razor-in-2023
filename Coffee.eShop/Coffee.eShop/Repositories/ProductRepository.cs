using Coffee.eShop.ApplicationCore.Interfaces;
using Coffee.eShop.Models;

namespace Coffee.eShop.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Americano", Price = 10.01M, Detail = "Coffee Americano", IsTrendingProduct = false, ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
        new Product { Id = 2, Name = "Cortado", Price = 10.01M, Detail = "Coffee Cortado", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
        new Product { Id = 3, Name = "Mocha", Price = 10.01M, Detail = "Coffee Mocha", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" }
    };

    //private CoffeeShopDbContext dbContext;
    //public ProductRepository(CoffeeShopDbContext dbContext)
    //{
    //    this.dbContext = dbContext;
    //}

    public IEnumerable<Product> GetAllProducts()
    {
        return _products;
    }

    public Product? GetProductDetail(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Product> GetTrendingProducts()
    {
        return _products.Where(p => p.IsTrendingProduct);
    }
}

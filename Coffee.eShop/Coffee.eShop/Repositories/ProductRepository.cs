using Coffee.eShop.ApplicationCore.Interfaces;
using Coffee.eShop.Models;

namespace Coffee.eShop.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Americano", Price = 10.01M, Detail = "Coffee Americano", IsTrendingProduct = false, ImageUrl = "https://unsplash.com/photos/nzyzAUsbV0M" },
        new Product { Id = 2, Name = "Cortado", Price = 10.01M, Detail = "Coffee Cortado", IsTrendingProduct = true, ImageUrl = "https://unsplash.com/photos/6VhPY27jdps" },
        new Product { Id = 3, Name = "Mocha", Price = 10.01M, Detail = "Coffee Mocha", IsTrendingProduct = true, ImageUrl = "https://unsplash.com/photos/IhqDpFz7I8Q" }
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

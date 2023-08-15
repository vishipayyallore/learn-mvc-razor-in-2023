using Coffee.eShop.ApplicationCore.Interfaces;
using Coffee.eShop.Data;
using Coffee.eShop.Models;

namespace Coffee.eShop.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CoffeeShopDbContext _coffeeShopDbContext;

    public ProductRepository(CoffeeShopDbContext coffeeShopDbContext)
    {
        _coffeeShopDbContext = coffeeShopDbContext ?? throw new ArgumentNullException(nameof(coffeeShopDbContext));
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _coffeeShopDbContext.Products.ToList();
    }

    public Product? GetProductDetail(int id)
    {
        return _coffeeShopDbContext.Products.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Product> GetTrendingProducts()
    {
        return _coffeeShopDbContext.Products.Where(p => p.IsTrendingProduct);
    }
}

//private readonly List<Product> _products = new()
//{
//    new Product { Id = 1, Name = "Americano", Price = 10.01M, Detail = "Coffee Americano", IsTrendingProduct = false, ImageUrl = "https://images.unsplash.com/photo-1511920170033-f8396924c348?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
//    new Product { Id = 2, Name = "Cortado", Price = 10.01M, Detail = "Coffee Cortado", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" },
//    new Product { Id = 3, Name = "Mocha", Price = 10.01M, Detail = "Coffee Mocha", IsTrendingProduct = true, ImageUrl = "https://images.unsplash.com/photo-1509042239860-f550ce710b93?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MXx8Q29mZmVlfGVufDB8fDB8fHwy&auto=format&fit=crop&w=500&q=60" }
//};

using Coffee.eShop.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.eShop.Controllers;

public class HomeController : Controller
{
    private readonly IProductRepository _productRepository;

    public HomeController(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public IActionResult Index()
    {
        return View(_productRepository.GetTrendingProducts());
    }
}

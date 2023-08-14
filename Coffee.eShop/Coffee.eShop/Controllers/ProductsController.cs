using Coffee.eShop.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.eShop.Controllers;

public class ProductsController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public IActionResult Shop()
    {
        return View(_productRepository.GetAllProducts());
    }

    public IActionResult Detail(int id)
    {
        var product = _productRepository.GetProductDetail(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
}

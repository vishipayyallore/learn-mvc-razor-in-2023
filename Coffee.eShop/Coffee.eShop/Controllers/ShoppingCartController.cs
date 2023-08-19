using Coffee.eShop.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.eShop.Controllers;

public class ShoppingCartController : Controller
{

    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;

    public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
    {
        _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));

        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public IActionResult Index()
    {
        var items = _shoppingCartRepository.GetShoppingCartItems();

        _shoppingCartRepository.ShoppingCartItems = items;

        ViewBag.CartTotal = _shoppingCartRepository.GetShoppingCartTotal();

        return View(items);
    }

    public RedirectToActionResult AddToShoppingCart(int pId)
    {
        var product = _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);

        if (product is not null)
        {
            _shoppingCartRepository.AddToCart(product);

            int cartCount = _shoppingCartRepository.GetShoppingCartItems().Count;

            HttpContext.Session.SetInt32("CartCount", cartCount);
        }

        return RedirectToAction("Index");
    }

    public RedirectToActionResult RemoveFromShoppingCart(int pId)
    {
        var product = _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);

        if (product is not null)
        {
            _shoppingCartRepository.RemoveFromCart(product);

            int cartCount = _shoppingCartRepository.GetShoppingCartItems().Count;

            HttpContext.Session.SetInt32("CartCount", cartCount);
        }

        return RedirectToAction("Index");
    }

}

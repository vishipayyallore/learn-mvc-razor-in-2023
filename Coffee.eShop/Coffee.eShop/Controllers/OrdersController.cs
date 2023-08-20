using Coffee.eShop.ApplicationCore.Interfaces;
using Coffee.eShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.eShop.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShoppingCartRepository _shopCartRepository;

    public OrdersController(IOrderRepository orderRepository, IShoppingCartRepository shopCartRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

        _shopCartRepository = shopCartRepository ?? throw new ArgumentNullException(nameof(shopCartRepository));
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        _orderRepository.PlaceOrder(order);

        _shopCartRepository.ClearCart();

        HttpContext.Session.SetInt32("CartCount", 0);

        return RedirectToAction(nameof(CheckoutComplete));
    }

    public IActionResult CheckoutComplete()
    {
        return View();
    }

}

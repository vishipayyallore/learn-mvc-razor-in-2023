using Microsoft.AspNetCore.Mvc;

namespace Coffee.eShop.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

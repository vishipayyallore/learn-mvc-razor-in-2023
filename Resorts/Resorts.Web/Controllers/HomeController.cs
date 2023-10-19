using Microsoft.AspNetCore.Mvc;
using Resorts.Application.Common.Interfaces;
using Resorts.Web.ViewModels;

namespace Resorts.Web.Controllers;

public class HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public IActionResult Index()
    {
        _logger.LogInformation("Starting HomeController::Index().");

        HomeVM homeVM = new()
        {
            VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity"),
            Nights = 1,
            CheckInDate = DateOnly.FromDateTime(DateTime.Now),
        };

        return View(homeVM);
    }

    [HttpPost]
    public IActionResult Index(HomeVM homeVM)
    {
        _logger.LogInformation("Starting HomeController::Index().");

        homeVM.VillaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity");

        return View(homeVM);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }

}


//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//public IActionResult Error()
//{
//    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//}

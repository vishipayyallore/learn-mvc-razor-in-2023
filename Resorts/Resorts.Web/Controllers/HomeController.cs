using Microsoft.AspNetCore.Mvc;
using Resorts.Application.Common.Interfaces;
using Resorts.Application.Common.Utility;
using Resorts.Domain.Entities;
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
    public IActionResult GetVillasByDate(int nights, DateOnly checkInDate)
    {
        Thread.Sleep(250);

        IEnumerable<Villa> villaList = _unitOfWork.Villa.GetAll(includeProperties: "VillaAmenity");
        List<VillaNumber> villaNumbersList = _unitOfWork.VillaNumber.GetAll().ToList();
        List<Booking> bookedVillas = _unitOfWork.Booking.GetAll(r => r.Status == SD.StatusApproved || r.Status == SD.StatusCheckedIn).ToList();

        foreach (var villa in villaList)
        {
            int roomAvailable = SD.VillaRoomsAvailable_Count(villa.Id, villaNumbersList, checkInDate,
                nights, bookedVillas);

            villa.IsAvailable = roomAvailable > 0;
        }

        HomeVM homeVM = new()
        {
            VillaList = villaList,
            Nights = nights,
            CheckInDate = checkInDate,
        };

        return PartialView("_VillaList", homeVM);
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

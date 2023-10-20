using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;

namespace Resorts.Web.Controllers;

public class BookingController(IUnitOfWork unitOfWork) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
    {
        Booking booking = new()
        {
            VillaId = villaId,
            Villa = _unitOfWork.Villa.Get(u => u.Id == villaId, includeProperties: "VillaAmenity"),
            CheckInDate = checkInDate,
            Nights = nights,
            CheckOutDate = checkInDate.AddDays(nights),
            UserId = string.Empty,
            Phone = string.Empty,
            Email = string.Empty,
            Name = string.Empty,
            TotalCost = 0,
            BookingDate = DateTime.UtcNow
        };
        booking.TotalCost = booking.Villa.Price * nights;

        return View(booking);
    }
}

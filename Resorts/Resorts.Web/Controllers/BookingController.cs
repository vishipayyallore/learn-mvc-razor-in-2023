using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using System.Security.Claims;
using WhiteLagoon.Application.Common.Utility;

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
        var claimsIdentity = (ClaimsIdentity)User.Identity!;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        ApplicationUser user = _unitOfWork.User.Get(u => u.Id == userId)!;

        Booking booking = new()
        {
            VillaId = villaId,
            Villa = _unitOfWork.Villa.Get(u => u.Id == villaId, includeProperties: "VillaAmenity"),
            CheckInDate = checkInDate,
            Nights = nights,
            CheckOutDate = checkInDate.AddDays(nights),
            UserId = userId,
            Phone = user.PhoneNumber,
            Email = user.Email,
            Name = user.Name,
            TotalCost = 0,
            BookingDate = DateTime.UtcNow
        };

        booking.TotalCost = booking.Villa.Price * nights;

        return View(booking);
    }

    [Authorize]
    [HttpPost]
    public IActionResult FinalizeBooking(Booking booking)
    {
        var villa = _unitOfWork.Villa.Get(r => r.Id == booking.VillaId);
        booking.TotalCost = villa!.Price * booking.Nights;

        booking.Status = SD.StatusPending;
        booking.BookingDate = DateTime.Now;

        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();

        return RedirectToAction(nameof(BookingConfirmation), new { bookingId = booking.Id });

        //if (!_villaService.IsVillaAvailableByDate(villa.Id, booking.Nights, booking.CheckInDate))
        //{
        //    TempData["error"] = "Room has been sold out!";
        //    //no rooms available
        //    return RedirectToAction(nameof(FinalizeBooking), new
        //    {
        //        villaId = booking.VillaId,
        //        checkInDate = booking.CheckInDate,
        //        nights = booking.Nights
        //    });
        //}

        //_bookingService.CreateBooking(booking);

        //var domain = Request.Scheme + "://" + Request.Host.Value + "/";

        //var options = _paymentService.CreateStripeSessionOptions(booking, villa, domain);

        //var session = _paymentService.CreateStripeSession(options);

        //_bookingService.UpdateStripePaymentID(booking.Id, session.Id, session.PaymentIntentId);
        //Response.Headers.Add("Location", session.Url);
        //return new StatusCodeResult(303);
    }

    [Authorize]
    public IActionResult BookingConfirmation(int bookingId)
    {
        return View(bookingId);
    }
}

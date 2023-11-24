using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resorts.Application.Common.Interfaces;
using Resorts.Application.Common.Utility;
using Resorts.Domain.Entities;
using Stripe.Checkout;
using System.Security.Claims;

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

        List<VillaNumber> villaNumbersList = _unitOfWork.VillaNumber.GetAll().ToList();
        List<Booking> bookedVillas = _unitOfWork.Booking.GetAll(r => r.Status == SD.StatusApproved || r.Status == SD.StatusCheckedIn).ToList();

        int roomAvailable = SD.VillaRoomsAvailable_Count(villa.Id, villaNumbersList, booking.CheckInDate,
            booking.Nights, bookedVillas);

        if (roomAvailable == 0)
        {
            TempData["Success"] = "Room has been sold out.";

            return RedirectToAction(nameof(FinalizeBooking), new
            {
                villaId = booking.VillaId,
                checkInDate = booking.CheckInDate,
                nights = booking.Nights
            });
        }

        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();

        var domain = Request.Scheme + "://" + Request.Host.Value + "/";
        var options = new SessionCreateOptions
        {
            LineItems = [],
            Mode = "payment",
            SuccessUrl = domain + $"booking/BookingConfirmation?bookingId={booking.Id}",
            CancelUrl = domain + $"booking/FinalizeBooking?villaId={booking.VillaId}&checkInDate={booking.CheckInDate}&nights={booking.Nights}",
        };

        options.LineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(booking.TotalCost * 100),
                Currency = "inr",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = villa.Name
                },
            },
            Quantity = 1,
        });

        var service = new SessionService();
        Session session = service.Create(options);

        _unitOfWork.Booking.UpdateStripePaymentID(booking.Id, session.Id, session.PaymentIntentId);
        _unitOfWork.Save();

        Response?.Headers?.Append("Location", session.Url);

        return new StatusCodeResult(303);
    }

    [Authorize]
    public IActionResult BookingConfirmation(int bookingId)
    {
        Booking bookingFromDb = _unitOfWork.Booking.Get(u => u.Id == bookingId, includeProperties: "User,Villa");

        if (bookingFromDb.Status == SD.StatusPending)
        {
            //this is a pending order, we need to confirm if payment was successful

            var service = new SessionService();
            Session session = service.Get(bookingFromDb.StripeSessionId);

            if (session.PaymentStatus == "paid")
            {
                _unitOfWork.Booking.UpdateStatus(bookingFromDb.Id, SD.StatusApproved, 0);

                _unitOfWork.Booking.UpdateStripePaymentID(bookingFromDb.Id, session.Id, session.PaymentIntentId);

                _unitOfWork.Save();
            }
        }

        return View(bookingId);
    }

    [Authorize]
    public IActionResult BookingDetails(int bookingId)
    {
        Booking bookingFromDb = _unitOfWork.Booking.Get(u => u.Id == bookingId, includeProperties: "User,Villa");

        if (bookingFromDb?.VillaNumber == 0 && bookingFromDb.Status == SD.StatusApproved)
        {
            var availableVillaNumber = AssignAvailableVillaNumberByVilla(bookingFromDb.VillaId);

            bookingFromDb.VillaNumbers = _unitOfWork.VillaNumber.GetAll(u => u.VillaId == bookingFromDb.VillaId
                && availableVillaNumber.Any(x => x == u.Villa_Number)).ToList();
        }

        return View(bookingFromDb);
    }

    [HttpPost]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult CheckIn(Booking booking)
    {
        _unitOfWork.Booking.UpdateStatus(booking.Id, SD.StatusCheckedIn, booking.VillaNumber);
        _unitOfWork.Save();

        TempData["Success"] = "Booking Updated Successfully.";

        return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
    }

    [HttpPost]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult CheckOut(Booking booking)
    {
        _unitOfWork.Booking.UpdateStatus(booking.Id, SD.StatusCompleted, booking.VillaNumber);
        _unitOfWork.Save();

        TempData["Success"] = "Booking Completed Successfully.";

        return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
    }

    [HttpPost]
    [Authorize(Roles = SD.Role_Admin)]
    public IActionResult CancelBooking(Booking booking)
    {
        _unitOfWork.Booking.UpdateStatus(booking.Id, SD.StatusCancelled, 0);
        _unitOfWork.Save();

        TempData["Success"] = "Booking Cancelled Successfully.";

        return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
    }

    private List<int> AssignAvailableVillaNumberByVilla(int villaId)
    {
        List<int> availableVillaNumbers = [];

        var villaNumbers = _unitOfWork.VillaNumber.GetAll(u => u.VillaId == villaId);

        var checkedInVilla = _unitOfWork.Booking
            .GetAll(u => u.VillaId == villaId && u.Status == SD.StatusCheckedIn)
            .Select(u => u.VillaNumber);

        foreach (var villaNumber in villaNumbers)
        {
            if (!checkedInVilla.Contains(villaNumber.Villa_Number))
            {
                availableVillaNumbers.Add(villaNumber.Villa_Number);
            }
        }
        return availableVillaNumbers;
    }

    #region API Calls
    [HttpGet]
    [Authorize]
    public IActionResult GetAll(string status)
    {
        IEnumerable<Booking> bookings;

        if (User.IsInRole(SD.Role_Admin))
        {
            bookings = _unitOfWork.Booking.GetAll(includeProperties: "User,Villa");
        }
        else
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            bookings = _unitOfWork.Booking.GetAll(u => u.UserId == userId, includeProperties: "User,Villa");
        }

        if (!string.IsNullOrEmpty(status))
        {
            bookings = bookings.Where(r => r.Status.ToLower() == status.ToLower());
        }

        return Json(new { data = bookings });
    }
    #endregion

}

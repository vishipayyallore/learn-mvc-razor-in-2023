using Resorts.Application.Common.Interfaces;
using Resorts.Application.Common.Utility;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;

namespace Resorts.Infrastructure.Repositories;

public class BookingRepository(ApplicationDbContext dbContext) : Repository<Booking>(dbContext), IBookingRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public void Update(Booking entity)
    {
        _dbContext.Bookings.Update(entity);
    }

    public void UpdateStatus(int bookingId, string bookingStatus, int villaNumber = 0)
    {
        var bookingFromDb = _dbContext.Bookings.FirstOrDefault(m => m.Id == bookingId);

        if (bookingFromDb is not null)
        {
            bookingFromDb.Status = bookingStatus;

            if (bookingStatus == SD.StatusCheckedIn)
            {
                bookingFromDb.VillaNumber = villaNumber;
                bookingFromDb.ActualCheckInDate = DateTime.Now;
            }

            if (bookingStatus == SD.StatusCompleted)
            {
                bookingFromDb.ActualCheckOutDate = DateTime.Now;
            }
        }
    }

    public void UpdateStripePaymentID(int bookingId, string sessionId, string paymentIntentId)
    {
        var bookingFromDb = _dbContext.Bookings.FirstOrDefault(m => m.Id == bookingId);

        if (bookingFromDb is not null)
        {
            if (!string.IsNullOrEmpty(sessionId))
            {
                bookingFromDb.StripeSessionId = sessionId;
            }

            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                bookingFromDb.StripePaymentIntentId = paymentIntentId;
                bookingFromDb.PaymentDate = DateTime.Now;
                bookingFromDb.IsPaymentSuccessful = true;
            }
        }
    }
}
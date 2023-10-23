using Resorts.Domain.Entities;

namespace Resorts.Application.Common.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    void Update(Booking entity);

    void UpdateStatus(int bookingId, string bookingStatus, int villaNumber);

    void UpdateStripePaymentID(int bookingId, string sessionId, string paymentIntentId);
}

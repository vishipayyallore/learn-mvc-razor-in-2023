using Resorts.Domain.Entities;

namespace Resorts.Application.Common.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    void Update(Booking entity);
}

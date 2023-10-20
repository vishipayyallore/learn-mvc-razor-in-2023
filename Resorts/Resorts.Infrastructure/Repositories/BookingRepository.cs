using Microsoft.EntityFrameworkCore;
using Resorts.Application.Common.Interfaces;
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
}
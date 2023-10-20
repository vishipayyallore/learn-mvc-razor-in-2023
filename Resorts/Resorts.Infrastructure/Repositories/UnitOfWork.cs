using Resorts.Application.Common.Interfaces;
using Resorts.Infrastructure.Data;

namespace Resorts.Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IVillaRepository Villa { get; private set; } = new VillaRepository(context);

    public IVillaNumberRepository VillaNumber { get; private set; } = new VillaNumberRepository(context);

    public IAmenityRepository Amenity { get; private set; } = new AmenityRepository(context);

    public IBookingRepository Booking { get; private set; } = new BookingRepository(context);

    public void Save()
    {
        _ = _context.SaveChanges();
    }
}

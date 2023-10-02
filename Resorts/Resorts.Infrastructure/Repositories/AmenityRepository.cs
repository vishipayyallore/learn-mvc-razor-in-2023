using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;

namespace Resorts.Infrastructure.Repositories;

public class AmenityRepository(ApplicationDbContext dbContext) : Repository<Amenity>(dbContext), IAmenityRepository
{
    private static readonly char[] separator = [','];
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public void Update(Amenity entity)
    {
        _ = _dbContext.Amenities.Update(entity);
    }
}

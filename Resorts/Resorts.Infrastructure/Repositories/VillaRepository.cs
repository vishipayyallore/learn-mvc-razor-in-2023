using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;

namespace Resorts.Infrastructure.Repositories;

public class VillaRepository(ApplicationDbContext dbContext) : Repository<Villa>(dbContext), IVillaRepository
{
    private static readonly char[] separator = [','];
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public void Update(Villa entity)
    {
        _ = _dbContext.Villas.Update(entity);
    }
}

using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;

namespace Resorts.Infrastructure.Repositories;

public class VillaNumberRepository(ApplicationDbContext dbContext) : Repository<VillaNumber>(dbContext), IVillaNumberRepository
{
    private static readonly char[] separator = [','];
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public void Update(VillaNumber entity)
    {
        _ = _dbContext.VillaNumbers.Update(entity);
    }
}

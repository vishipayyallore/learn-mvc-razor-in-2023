using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;
using System.Linq.Expressions;

namespace Resorts.Infrastructure.Repositories;

public class VillaRepository(ApplicationDbContext dbContext) : IVillaRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public void Add(Villa villa)
    {
        _ = _dbContext.Add(villa);
    }

    public IEnumerable<Villa> Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
    {
        throw new NotImplementedException();
    }

    public void Remove(Villa villa)
    {
        _ = (_dbContext?.Remove(villa));
    }

    public void Save()
    {
        _ = _dbContext.SaveChanges();
    }

    public void Update(Villa villa)
    {
        _ = _dbContext.Villas.Update(villa);
    }
}

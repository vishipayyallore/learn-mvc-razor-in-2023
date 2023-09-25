using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using System.Linq.Expressions;

namespace Resorts.Infrastructure.Repositories;

public class VillaRepository : IVillaRepository
{
    public void Add(Villa villa)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void Update(Villa villa)
    {
        throw new NotImplementedException();
    }
}

using Resorts.Application.Common.Interfaces;
using System.Linq.Expressions;

namespace Resorts.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    public void Add(T villa)
    {
        throw new NotImplementedException();
    }

    public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        throw new NotImplementedException();
    }

    public void Remove(T villa)
    {
        throw new NotImplementedException();
    }
}

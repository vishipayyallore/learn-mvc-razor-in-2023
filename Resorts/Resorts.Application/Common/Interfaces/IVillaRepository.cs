using Resorts.Domain.Entities;
using System.Linq.Expressions;

namespace Resorts.Application.Common.Interfaces;

public interface IVillaRepository
{
    IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null);

    Villa? Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null);

    void Add(Villa villa);

    void Update(Villa villa);

    void Remove(Villa villa);

    void Save();
}

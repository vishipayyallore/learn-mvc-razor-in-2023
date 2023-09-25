using Resorts.Domain.Entities;
using System.Linq.Expressions;

namespace Resorts.Application.Common.Interfaces;

public interface IVillaRepository
{
    IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null, bool tracked = false);
}

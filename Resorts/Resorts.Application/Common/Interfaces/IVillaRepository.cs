using Resorts.Domain.Entities;

namespace Resorts.Application.Common.Interfaces;

public interface IVillaRepository : IRepository<Villa>
{
    void Update(Villa entity);
}

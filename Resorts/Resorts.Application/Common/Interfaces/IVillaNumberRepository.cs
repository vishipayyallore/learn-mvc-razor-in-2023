using Resorts.Domain.Entities;

namespace Resorts.Application.Common.Interfaces;

public interface IVillaNumberRepository : IRepository<VillaNumber>
{
    void Update(VillaNumber entity);

    void Save();
}

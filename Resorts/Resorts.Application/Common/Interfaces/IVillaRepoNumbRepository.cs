using Resorts.Domain.Entities;

namespace Resorts.Application.Common.Interfaces;

public interface IVillaRepoNumbRepository : IRepository<VillaNumber>
{
    void Update(VillaNumber entity);

    void Save();
}

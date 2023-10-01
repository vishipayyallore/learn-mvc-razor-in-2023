using Resorts.Domain.Entities;

namespace Resorts.Application.Common.Interfaces;

public interface IAmenityRepository : IRepository<Amenity>
{
    void Update(Amenity entity);

    void Save();
}

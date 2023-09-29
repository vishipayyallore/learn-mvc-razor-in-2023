using Resorts.Application.Common.Interfaces;
using Resorts.Infrastructure.Data;

namespace Resorts.Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public IVillaRepository Villa { get; private set; } = new VillaRepository(context);

    public void Save()
    {
        _ = _context.SaveChanges();
    }
}

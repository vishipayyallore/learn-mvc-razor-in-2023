using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;

namespace Resorts.Infrastructure.Repositories;

public class ApplicationUserRepository(ApplicationDbContext dbContext) : Repository<ApplicationUser>(dbContext), IApplicationUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
}

using TicketsManagement.Domain.Entities;

namespace TicketsManagement.Application.Contracts.Persistence;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents);
}

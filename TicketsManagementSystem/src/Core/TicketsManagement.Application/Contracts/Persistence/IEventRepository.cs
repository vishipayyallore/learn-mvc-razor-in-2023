using TicketsManagement.Domain.Entities;

namespace TicketsManagement.Application.Contracts.Persistence;

public interface IEventRepository : IGenericRepository<Event>
{
    Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate);
}

namespace TicketsManagement.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetById(Guid id);

    Task<IReadOnlyList<T>> ListAll();

    Task<T> Add(T entity);

    Task Update(T entity);

    Task Delete(T entity);

    Task<IReadOnlyList<T>> GetPagedData(int page, int size);
}

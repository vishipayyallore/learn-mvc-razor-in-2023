using Microsoft.EntityFrameworkCore;
using Resorts.Application.Common.Interfaces;
using Resorts.Infrastructure.Data;
using System.Linq.Expressions;

namespace Resorts.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private static readonly char[] separator = [','];
    private readonly ApplicationDbContext _context;
    internal DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _dbSet = _context.Set<T>();
    }

    public void Add(T entity)
    {
        _ = _dbSet?.Add(entity);
    }

    public bool Any(Expression<Func<T, bool>> filter)
    {
        return _dbSet.All(filter);
    }

    public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var property in includeProperties.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var property in includeProperties.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        return query.ToList();
    }

    public void Remove(T entity)
    {
        _ = _dbSet?.Remove(entity);
    }
}

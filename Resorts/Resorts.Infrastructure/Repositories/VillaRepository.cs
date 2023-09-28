using Microsoft.EntityFrameworkCore;
using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;
using System.Linq.Expressions;

namespace Resorts.Infrastructure.Repositories;

public class VillaRepository(ApplicationDbContext dbContext) : IVillaRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private static readonly char[] separator = [','];

    public void Add(Villa villa)
    {
        _ = _dbContext.Add(villa);
    }

    public Villa? Get(Expression<Func<Villa, bool>> filter, string? includeProperties = null)
    {
        IQueryable<Villa> query = _dbContext.Set<Villa>().AsQueryable();

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

    public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<Villa> query = _dbContext.Set<Villa>().AsQueryable();

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

    public void Remove(Villa villa)
    {
        _ = (_dbContext?.Remove(villa));
    }

    public void Save()
    {
        _ = _dbContext.SaveChanges();
    }

    public void Update(Villa villa)
    {
        _ = _dbContext.Villas.Update(villa);
    }
}

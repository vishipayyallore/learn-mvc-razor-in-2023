﻿using System.Linq.Expressions;

namespace Resorts.Application.Common.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

    T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

    void Add(T villa);

    bool Any(Expression<Func<T, bool>> filter);

    void Remove(T villa);
}

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Data.Context;

namespace Ordering.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected readonly OrderContext DbContext;

    public Repository(OrderContext dbContext) => DbContext = dbContext;

    public async Task<IReadOnlyList<T>> GetAllAsync() => await DbContext.Set<T>().ToListAsync().ConfigureAwait(false);

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate) => await DbContext.Set<T>().Where(predicate).ToListAsync().ConfigureAwait(false);

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeString = null!,
        bool disableTracking = true
    )
    {
        IQueryable<T> query = DbContext.Set<T>();
        if (disableTracking)
            query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString))
            query = query.Include(includeString);

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync().ConfigureAwait(false);

        return await query.ToListAsync().ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null,
        bool disableTracking = true
    )
    {
        IQueryable<T> query = DbContext.Set<T>();
        if (disableTracking) query = query.AsNoTracking();

        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync().ConfigureAwait(false);

        return await query.ToListAsync().ConfigureAwait(false);
    }

    public virtual async Task<T> GetByIdAsync(int id) => ( await DbContext.Set<T>().FindAsync(id).ConfigureAwait(false) )!;

    public async Task<T> AddAsync(T entity)
    {
        DbContext.Set<T>().Add(entity);
        await DbContext.SaveChangesAsync().ConfigureAwait(false);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}

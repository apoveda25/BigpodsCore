using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace Bigpods.Monolith.Modules.Shared.Domain.Database;

public interface IGenericRepository<T> where T : class
{
    DbSet<T> Model { get; }

    Task<T?> FindOneAsync(
        Expression<Func<T, bool>>? filter = null,
        string[]? includeProperties = null,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<T>> FindManyAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string[]? includeProperties = null,
        CancellationToken cancellationToken = default
    );

    Task<T> CreateOneAsync(T entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    T DeleteOne(T entity);

    IEnumerable<T> DeleteMany(IEnumerable<T> entities);

    T UpdateOne(T entity);

    IEnumerable<T> UpdateMany(IEnumerable<T> entities);
}

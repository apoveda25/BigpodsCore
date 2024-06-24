using System.Linq.Expressions;

using Bigpods.Monolith.Modules.Shared.Domain.Database;

using Microsoft.EntityFrameworkCore;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Database;

public class GenericRepository<T>(DatabaseService context, ILogger logger) : IGenericRepository<T> where T : class
{
    protected readonly DatabaseService _context = context;
    protected readonly ILogger _logger = logger;
    protected readonly DbSet<T> _model = context.Set<T>();

    public DbSet<T> Model { get => _model; }

    public async Task<T?> FindOneAsync(
        Expression<Func<T, bool>>? filter = null,
        string[]? includeProperties = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            IQueryable<T> query = _model.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting entity");
            throw;
        }
    }

    public async Task<IEnumerable<T>> FindManyAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string[]? includeProperties = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            IQueryable<T> query = _model.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting entity");
            throw;
        }
    }

    public async Task<T> CreateOneAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _model.AddAsync(entity, cancellationToken);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding entity {entity}", entity);
            throw;
        }
    }

    public async Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        try
        {

            await _model.AddRangeAsync(entities, cancellationToken);

            return entities;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding entities");
            throw;
        }
    }

    public T UpdateOne(T entity)
    {
        try
        {
            _model.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating entity {entity}", entity);
            throw;
        }
    }

    public IEnumerable<T> UpdateMany(IEnumerable<T> entities)
    {
        try
        {
            _model.UpdateRange(entities);

            return entities;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating entities");
            throw;
        }
    }

    public T DeleteOne(T entity)
    {
        try
        {
            _model.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting entity {entity}", entity);
            throw;
        }
    }

    public IEnumerable<T> DeleteMany(IEnumerable<T> entities)
    {
        try
        {
            _model.UpdateRange(entities);

            return entities;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting entities");
            throw;
        }
    }
}

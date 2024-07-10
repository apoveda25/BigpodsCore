namespace Bigpods.Monolith.Modules.Shared.Domain.Database;

public interface IUnitOfWork
{
    IGenericRepository<T> GetRepository<T>()
        where T : class, new();

    Task CompleteAsync(CancellationToken cancellationToken = default);
}

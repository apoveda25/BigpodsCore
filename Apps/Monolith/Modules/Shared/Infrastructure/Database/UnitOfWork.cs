using Bigpods.Monolith.Modules.Shared.Domain.Database;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Database;

public sealed class UnitOfWork(DatabaseService context, ILoggerFactory loggerFactory)
    : IUnitOfWork,
        IDisposable
{
    private readonly DatabaseService _context = context;
    private readonly ILogger _logger = loggerFactory.CreateLogger($"UnitOfWork");

    private readonly Dictionary<string, object> _repositories = [];

    public IGenericRepository<T> GetRepository<T>()
        where T : class, new()
    {
        var existRepository = _repositories.TryGetValue(typeof(T).Name, out var value);

        return (IGenericRepository<T>)(
            existRepository
                ? _repositories[typeof(T).Name]
                : new GenericRepository<T>(_context, _logger)
        );
    }

    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();

        GC.SuppressFinalize(this);
    }
}

using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Commands;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Services;

public interface IUpdateOneWarehouseService
{
    public Task<IUpdateOneWarehouseServiceResponse> ExecuteAsync(
        IUpdateOneWarehouseCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IUpdateOneWarehouseServiceResponse
{
    public IWarehouseModel? WarehouseFoundById { get; init; }
    public IWarehouseModel? WarehouseFoundByName { get; init; }
}

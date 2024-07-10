using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Commands;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Services;

public interface ICreateOneWarehouseService
{
    public Task<ICreateOneWarehouseServiceResponse> ExecuteAsync(
        ICreateOneWarehouseCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface ICreateOneWarehouseServiceResponse
{
    public IWarehouseModel? WarehouseFoundById { get; init; }
    public IWarehouseModel? WarehouseFoundByName { get; init; }
}

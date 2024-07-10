using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Commands;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Services;

public interface IDeleteOneWarehouseService
{
    public Task<IDeleteOneWarehouseServiceResponse> ExecuteAsync(
        IDeleteOneWarehouseCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IDeleteOneWarehouseServiceResponse
{
    public IWarehouseModel? WarehouseFoundById { get; init; }
    public IInventoryModel[] InventoriesFoundByWarehouseId { get; init; }
    public IInventoryInputModel[] InventoryInputsFoundByWarehouseId { get; init; }
    public IInventoryOutputModel[] InventoryOutputsFoundByWarehouseId { get; init; }
}

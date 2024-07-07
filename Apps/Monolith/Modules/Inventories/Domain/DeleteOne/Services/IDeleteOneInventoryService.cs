using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Services;

public interface IDeleteOneInventoryService
{
    public Task<IDeleteOneInventoryServiceResponse> ExecuteAsync(
        IDeleteOneInventoryCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IDeleteOneInventoryServiceResponse
{
    public IInventoryModel? InventoryFoundById { get; init; }
    public IWarehouseModel? WarehouseFoundById { get; init; }
    public IInventoryModel[] InventoriesFoundByProductIdWarehouseId { get; init; }
    public IInventoryInputModel[] InventoryInputsFoundByInventoryId { get; init; }
    public IInventoryOutputModel[] InventoryOutputsFoundByInventoryId { get; init; }
}
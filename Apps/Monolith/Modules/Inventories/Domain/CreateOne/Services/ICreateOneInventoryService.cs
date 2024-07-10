using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Services;

public interface ICreateOneInventoryService
{
    public Task<ICreateOneInventoryServiceResponse> ExecuteAsync(
        ICreateOneInventoryCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface ICreateOneInventoryServiceResponse
{
    public IInventoryModel? InventoryFoundById { get; init; }
    public IInventoryModel? InventoryFoundByWarehouseIdVariantId { get; init; }
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel? VariantFoundById { get; init; }
    public IWarehouseModel? WarehouseFoundById { get; init; }
}

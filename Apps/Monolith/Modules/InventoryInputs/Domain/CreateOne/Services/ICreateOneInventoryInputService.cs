using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Services;

public interface ICreateOneInventoryInputService
{
    public Task<ICreateOneInventoryInputServiceResponse> ExecuteAsync(
        ICreateOneInventoryInputCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface ICreateOneInventoryInputServiceResponse
{
    public IInventoryInputModel? InventoryInputFoundById { get; init; }
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel? VariantFoundById { get; init; }
    public IWarehouseModel? WarehouseFoundById { get; init; }
    public IInventoryModel? InventoryFoundById { get; init; }
}
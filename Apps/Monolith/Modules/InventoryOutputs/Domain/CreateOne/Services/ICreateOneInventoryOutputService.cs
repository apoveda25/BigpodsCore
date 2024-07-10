using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Services;

public interface ICreateOneInventoryOutputService
{
    public Task<ICreateOneInventoryOutputServiceResponse> ExecuteAsync(
        ICreateOneInventoryOutputCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface ICreateOneInventoryOutputServiceResponse
{
    public IInventoryOutputModel? InventoryOutputFoundById { get; init; }
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel? VariantFoundById { get; init; }
    public IWarehouseModel? WarehouseFoundById { get; init; }
    public IInventoryModel? InventoryFoundById { get; init; }
}

using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Services;

public sealed class CreateOneInventoryService(
    [Service] IUnitOfWork unitOfWork
) : ICreateOneInventoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICreateOneInventoryServiceResponse> ExecuteAsync(
        ICreateOneInventoryCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();

        var inventoryFoundById = await inventoriesRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryDto.Id,
            cancellationToken: cancellationToken
        );
        var productFoundById = await productsRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryDto.ProductId,
            cancellationToken: cancellationToken
        );
        var variantFoundById = await variantsRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryDto.VariantId,
            cancellationToken: cancellationToken
        );
        var warehouseFoundById = await warehousesRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryDto.WarehouseId,
            cancellationToken: cancellationToken
        );
        var inventoriesFoundByProductIdWarehouseId = await inventoriesRepository.FindManyAsync(
            filter: x => x.ProductId == command.InventoryDto.ProductId && x.WarehouseId == command.InventoryDto.WarehouseId,
            cancellationToken: cancellationToken
        );

        return new CreateOneInventoryServiceResponse(
            InventoryFoundById: inventoryFoundById,
            ProductFoundById: productFoundById,
            VariantFoundById: variantFoundById,
            WarehouseFoundById: warehouseFoundById,
            InventoriesFoundByProductIdWarehouseId: inventoriesFoundByProductIdWarehouseId.ToArray()
        );
    }
}

public sealed record CreateOneInventoryServiceResponse(
    IInventoryModel? InventoryFoundById,
    IProductModel? ProductFoundById,
    IVariantModel? VariantFoundById,
    IWarehouseModel? WarehouseFoundById,
    IInventoryModel[] InventoriesFoundByProductIdWarehouseId
) : ICreateOneInventoryServiceResponse;
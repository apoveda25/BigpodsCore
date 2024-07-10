using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Services;

public sealed class CreateOneInventoryService([Service] IUnitOfWork unitOfWork)
    : ICreateOneInventoryService
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
        var inventoryFoundByWarehouseIdVariantId = await inventoriesRepository.FindOneAsync(
            filter: x =>
                x.WarehouseId == command.InventoryDto.WarehouseId
                && x.VariantId == command.InventoryDto.VariantId,
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

        return new CreateOneInventoryServiceResponse(
            InventoryFoundById: inventoryFoundById,
            InventoryFoundByWarehouseIdVariantId: inventoryFoundByWarehouseIdVariantId,
            ProductFoundById: productFoundById,
            VariantFoundById: variantFoundById,
            WarehouseFoundById: warehouseFoundById
        );
    }
}

public sealed record CreateOneInventoryServiceResponse(
    IInventoryModel? InventoryFoundById,
    IInventoryModel? InventoryFoundByWarehouseIdVariantId,
    IProductModel? ProductFoundById,
    IVariantModel? VariantFoundById,
    IWarehouseModel? WarehouseFoundById
) : ICreateOneInventoryServiceResponse;

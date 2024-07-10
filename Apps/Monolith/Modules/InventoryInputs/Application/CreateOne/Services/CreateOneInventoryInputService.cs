using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Services;

public sealed class CreateOneInventoryInputService(
    [Service] IUnitOfWork unitOfWork
) : ICreateOneInventoryInputService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICreateOneInventoryInputServiceResponse> ExecuteAsync(
        ICreateOneInventoryInputCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var inventoryInputsRepository = _unitOfWork.GetRepository<InventoryInputModel>();
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();
        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();

        var inventoryInputFoundById = await inventoryInputsRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryInputDto.Id,
            cancellationToken: cancellationToken
        );
        var productFoundById = await productsRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryInputDto.ProductId,
            cancellationToken: cancellationToken
        );
        var variantFoundById = await variantsRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryInputDto.VariantId,
            cancellationToken: cancellationToken
        );
        var warehouseFoundById = await warehousesRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryInputDto.WarehouseId,
            cancellationToken: cancellationToken
        );
        var inventoryFoundById = await inventoriesRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryInputDto.InventoryId,
            cancellationToken: cancellationToken
        );

        return new CreateOneInventoryInputServiceResponse(
            InventoryInputFoundById: inventoryInputFoundById,
            ProductFoundById: productFoundById,
            VariantFoundById: variantFoundById,
            WarehouseFoundById: warehouseFoundById,
            InventoryFoundById: inventoryFoundById
        );
    }
}

public sealed record CreateOneInventoryInputServiceResponse(
    IInventoryInputModel? InventoryInputFoundById,
    IProductModel? ProductFoundById,
    IVariantModel? VariantFoundById,
    IWarehouseModel? WarehouseFoundById,
    IInventoryModel? InventoryFoundById
) : ICreateOneInventoryInputServiceResponse;
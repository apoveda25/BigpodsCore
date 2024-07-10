using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Services;

public sealed class CreateOneInventoryOutputService(
    [Service] IUnitOfWork unitOfWork
) : ICreateOneInventoryOutputService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICreateOneInventoryOutputServiceResponse> ExecuteAsync(
        ICreateOneInventoryOutputCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var inventoryInputsRepository = _unitOfWork.GetRepository<InventoryOutputModel>();
        var productsRepository = _unitOfWork.GetRepository<ProductModel>();
        var variantsRepository = _unitOfWork.GetRepository<VariantModel>();
        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();
        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();

        var inventoryInputFoundById = await inventoryInputsRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryOutputDto.Id,
            cancellationToken: cancellationToken
        );
        var productFoundById = await productsRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryOutputDto.ProductId,
            cancellationToken: cancellationToken
        );
        var variantFoundById = await variantsRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryOutputDto.VariantId,
            cancellationToken: cancellationToken
        );
        var warehouseFoundById = await warehousesRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryOutputDto.WarehouseId,
            cancellationToken: cancellationToken
        );
        var inventoryFoundById = await inventoriesRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryOutputDto.InventoryId,
            cancellationToken: cancellationToken
        );

        return new CreateOneInventoryOutputServiceResponse(
            InventoryOutputFoundById: inventoryInputFoundById,
            ProductFoundById: productFoundById,
            VariantFoundById: variantFoundById,
            WarehouseFoundById: warehouseFoundById,
            InventoryFoundById: inventoryFoundById
        );
    }
}

public sealed record CreateOneInventoryOutputServiceResponse(
    IInventoryOutputModel? InventoryOutputFoundById,
    IProductModel? ProductFoundById,
    IVariantModel? VariantFoundById,
    IWarehouseModel? WarehouseFoundById,
    IInventoryModel? InventoryFoundById
) : ICreateOneInventoryOutputServiceResponse;
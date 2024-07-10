using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

namespace Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Services;

public sealed class DeleteOneInventoryService([Service] IUnitOfWork unitOfWork)
    : IDeleteOneInventoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IDeleteOneInventoryServiceResponse> ExecuteAsync(
        IDeleteOneInventoryCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();
        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();
        var inventoriesInputsRepository = _unitOfWork.GetRepository<InventoryInputModel>();
        var inventoriesOutputsRepository = _unitOfWork.GetRepository<InventoryOutputModel>();

        var inventoryFoundById = await inventoriesRepository.FindOneAsync(
            filter: x => x.Id == command.InventoryDto.Id,
            cancellationToken: cancellationToken
        );
        var warehouseFoundById = inventoryFoundById is null
            ? null
            : await warehousesRepository.FindOneAsync(
                filter: x => x.Id == inventoryFoundById.WarehouseId,
                cancellationToken: cancellationToken
            );
        var inventoriesFoundByProductIdWarehouseId = inventoryFoundById is null
            ? []
            : await inventoriesRepository.FindManyAsync(
                filter: x =>
                    x.ProductId == inventoryFoundById.ProductId
                    && x.WarehouseId == inventoryFoundById.WarehouseId,
                cancellationToken: cancellationToken
            );
        var inventoryInputsFoundByInventoryId = await inventoriesInputsRepository.FindManyAsync(
            filter: x => x.InventoryId == command.InventoryDto.Id,
            cancellationToken: cancellationToken
        );
        var inventoryOutputsFoundByInventoryId = await inventoriesOutputsRepository.FindManyAsync(
            filter: x => x.InventoryId == command.InventoryDto.Id,
            cancellationToken: cancellationToken
        );

        return new DeleteOneInventoryServiceResponse(
            InventoryFoundById: inventoryFoundById,
            WarehouseFoundById: warehouseFoundById,
            InventoriesFoundByProductIdWarehouseId: inventoriesFoundByProductIdWarehouseId.ToArray(),
            InventoryInputsFoundByInventoryId: inventoryInputsFoundByInventoryId.ToArray(),
            InventoryOutputsFoundByInventoryId: inventoryOutputsFoundByInventoryId.ToArray()
        );
    }
}

public sealed record DeleteOneInventoryServiceResponse(
    IInventoryModel? InventoryFoundById,
    IWarehouseModel? WarehouseFoundById,
    IInventoryModel[] InventoriesFoundByProductIdWarehouseId,
    IInventoryInputModel[] InventoryInputsFoundByInventoryId,
    IInventoryOutputModel[] InventoryOutputsFoundByInventoryId
) : IDeleteOneInventoryServiceResponse;

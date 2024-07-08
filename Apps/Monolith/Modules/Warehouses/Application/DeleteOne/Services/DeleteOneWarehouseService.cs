using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Services;

namespace Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Services;

public sealed class DeleteOneWarehouseService(
    [Service] IUnitOfWork unitOfWork
) : IDeleteOneWarehouseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IDeleteOneWarehouseServiceResponse> ExecuteAsync(
        IDeleteOneWarehouseCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();
        var inventariesRepository = _unitOfWork.GetRepository<InventoryModel>();
        var inventoryInputsRepository = _unitOfWork.GetRepository<InventoryInputModel>();
        var inventoryOutputsRepository = _unitOfWork.GetRepository<InventoryOutputModel>();

        var warehouseFoundById = await warehousesRepository.FindOneAsync(
            filter: x => x.Id == command.WarehouseDto.Id,
            cancellationToken: cancellationToken
        );
        var inventoriesFoundByWarehouseId = warehouseFoundById is null ? null : await inventariesRepository.FindManyAsync(
            filter: x => x.WarehouseId == warehouseFoundById.Id,
            cancellationToken: cancellationToken
        );
        var inventoryInputsFoundByWarehouseId = warehouseFoundById is null ? null : await inventoryInputsRepository.FindManyAsync(
            filter: x => x.WarehouseId == warehouseFoundById.Id,
            cancellationToken: cancellationToken
        );
        var inventoryOutputsFoundByWarehouseId = warehouseFoundById is null ? null : await inventoryOutputsRepository.FindManyAsync(
            filter: x => x.WarehouseId == warehouseFoundById.Id,
            cancellationToken: cancellationToken
        );

        return new DeleteOneWarehouseServiceResponse(
            WarehouseFoundById: warehouseFoundById,
            InventoriesFoundByWarehouseId: inventoriesFoundByWarehouseId?.ToArray() ?? [],
            InventoryInputsFoundByWarehouseId: inventoryInputsFoundByWarehouseId?.ToArray() ?? [],
            InventoryOutputsFoundByWarehouseId: inventoryOutputsFoundByWarehouseId?.ToArray() ?? []
        );
    }
}

public sealed record DeleteOneWarehouseServiceResponse(
    IWarehouseModel? WarehouseFoundById,
    IInventoryModel[] InventoriesFoundByWarehouseId,
    IInventoryInputModel[] InventoryInputsFoundByWarehouseId,
    IInventoryOutputModel[] InventoryOutputsFoundByWarehouseId
) : IDeleteOneWarehouseServiceResponse;
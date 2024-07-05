using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Services;

using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Commands;

public sealed class DeleteOneWarehouseHandler(
    [Service] IDeleteOneWarehouseService deleteOneWarehouseService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<DeleteOneWarehouseCommand, WarehouseModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IDeleteOneWarehouseService _deleteOneWarehouseService = deleteOneWarehouseService;

    public async Task<WarehouseModel> Handle(
        DeleteOneWarehouseCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var fetchResponse = await _deleteOneWarehouseService.ExecuteAsync(command: command, cancellationToken: cancellationToken);

        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();

        var aggregateRoot = WarehouseAggregateRoot.DeleteOne(
            warehouse: command.WarehouseDto,
            warehouseFoundById: fetchResponse.WarehouseFoundById,
            inventoriesFoundByWarehouseId: fetchResponse.InventoriesFoundByWarehouseId,
            inventoryInputsFoundByWarehouseId: fetchResponse.InventoryInputsFoundByWarehouseId,
            inventoryOutputsFoundByWarehouseId: fetchResponse.InventoryOutputsFoundByWarehouseId
        );

        var warehouseModel = _mapper.Map<WarehouseModel>(source: aggregateRoot);

        warehousesRepository.DeleteOne(entity: warehouseModel);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return warehouseModel;
    }
}

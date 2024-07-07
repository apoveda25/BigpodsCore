using AutoMapper;

using Bigpods.Monolith.Modules.Inventories.Domain.Common.Aggregates;

using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using MediatR;

namespace Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Commands;

public sealed class DeleteOneInventoryHandler(
    [Service] IDeleteOneInventoryService deleteOneInventoryService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<DeleteOneInventoryCommand, InventoryModel>
{
    private readonly IDeleteOneInventoryService _deleteOneInventoryService = deleteOneInventoryService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<InventoryModel> Handle(
        DeleteOneInventoryCommand command,
        CancellationToken token = default
    )
    {
        var fetchResponse = await _deleteOneInventoryService.ExecuteAsync(command: command, cancellationToken: token);

        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();

        var aggregateRoot = WarehouseAggregateRoot.Build(warehouse: fetchResponse.WarehouseFoundById);

        var inventoryEntity = aggregateRoot.DeleteOneVariant(
            inventory: command.InventoryDto,
            inventoryFoundById: fetchResponse.InventoryFoundById,
            inventoriesFoundByProductIdWarehouseId: fetchResponse.InventoriesFoundByProductIdWarehouseId,
            inventoryInputsFoundByInventoryId: fetchResponse.InventoryInputsFoundByInventoryId,
            inventoryOutputsFoundByInventoryId: fetchResponse.InventoryOutputsFoundByInventoryId
        );

        var inventoryModel = _mapper.Map<InventoryModel>(
            source: inventoryEntity
        );

        inventoriesRepository.DeleteOne(
            entity: inventoryModel
        );

        await _unitOfWork.CompleteAsync(cancellationToken: token);

        return inventoryModel;
    }
}

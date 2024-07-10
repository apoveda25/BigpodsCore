using AutoMapper;
using Bigpods.Monolith.Modules.Inventories.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Commands;

public sealed class CreateOneInventoryHandler(
    [Service] ICreateOneInventoryService createOneInventoryService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<CreateOneInventoryCommand, InventoryModel>
{
    private readonly ICreateOneInventoryService _createOneInventoryService =
        createOneInventoryService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<InventoryModel> Handle(
        CreateOneInventoryCommand command,
        CancellationToken token = default
    )
    {
        var fetchResponse = await _createOneInventoryService.ExecuteAsync(
            command: command,
            cancellationToken: token
        );

        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();

        var aggregateRoot = WarehouseAggregateRoot.CreateOneInventory(
            command: command,
            data: fetchResponse
        );

        var inventoryModel = _mapper.Map<InventoryModel>(
            source: aggregateRoot.Inventories.FirstOrDefault(x => x.Id == command.InventoryDto.Id)
        );

        await inventoriesRepository.CreateOneAsync(
            entity: inventoryModel,
            cancellationToken: token
        );

        await _unitOfWork.CompleteAsync(cancellationToken: token);

        return inventoryModel;
    }
}

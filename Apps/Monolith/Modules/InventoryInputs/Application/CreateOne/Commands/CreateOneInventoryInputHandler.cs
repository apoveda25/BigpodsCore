using AutoMapper;

using Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Aggregates;

using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using MediatR;

namespace Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Commands;

public sealed class CreateOneInventoryInputHandler(
    [Service] ICreateOneInventoryInputService createOneInventoryInputService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<CreateOneInventoryInputCommand, InventoryInputModel>
{
    private readonly ICreateOneInventoryInputService _createOneInventoryInputService = createOneInventoryInputService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<InventoryInputModel> Handle(
        CreateOneInventoryInputCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var fetchResponse = await _createOneInventoryInputService.ExecuteAsync(
            command,
            cancellationToken
        );

        var inventoryInputsRepository = _unitOfWork.GetRepository<InventoryInputModel>();
        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();

        var aggregateRoot = WarehouseAggregateRoot.CreateOneInventoryInput(
            inventoryInput: command.InventoryInputDto,
            data: fetchResponse
        );

        var inventoryInputModel = _mapper.Map<InventoryInputModel>(
            aggregateRoot.InventoryInputs.FirstOrDefault(x => x.Id == command.InventoryInputDto.Id)
        );
        var inventoryModel = _mapper.Map<InventoryModel>(
            aggregateRoot.Inventories.FirstOrDefault(x => x.Id == command.InventoryInputDto.InventoryId)
        );

        await inventoryInputsRepository.CreateOneAsync(entity: inventoryInputModel, cancellationToken: cancellationToken);
        inventoriesRepository.UpdateOne(entity: inventoryModel);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return inventoryInputModel;
    }
}

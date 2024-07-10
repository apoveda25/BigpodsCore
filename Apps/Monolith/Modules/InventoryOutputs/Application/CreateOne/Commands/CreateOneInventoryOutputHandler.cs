using AutoMapper;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using MediatR;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Commands;

public sealed class CreateOneInventoryOutputHandler(
    [Service] ICreateOneInventoryOutputService createOneInventoryOutputService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<CreateOneInventoryOutputCommand, InventoryOutputModel>
{
    private readonly ICreateOneInventoryOutputService _createOneInventoryOutputService =
        createOneInventoryOutputService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<InventoryOutputModel> Handle(
        CreateOneInventoryOutputCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var fetchResponse = await _createOneInventoryOutputService.ExecuteAsync(
            command,
            cancellationToken
        );

        var inventoryOutputsRepository = _unitOfWork.GetRepository<InventoryOutputModel>();
        var inventoriesRepository = _unitOfWork.GetRepository<InventoryModel>();

        var aggregateRoot = WarehouseAggregateRoot.CreateOneInventoryOutput(
            command: command,
            data: fetchResponse
        );

        var inventoryOutputModel = _mapper.Map<InventoryOutputModel>(
            aggregateRoot.InventoryOutputs.FirstOrDefault(x =>
                x.Id == command.InventoryOutputDto.Id
            )
        );
        var inventoryModel = _mapper.Map<InventoryModel>(
            aggregateRoot.Inventories.FirstOrDefault(x =>
                x.Id == command.InventoryOutputDto.InventoryId
            )
        );

        await inventoryOutputsRepository.CreateOneAsync(
            entity: inventoryOutputModel,
            cancellationToken: cancellationToken
        );
        inventoriesRepository.UpdateOne(entity: inventoryModel);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return inventoryOutputModel;
    }
}

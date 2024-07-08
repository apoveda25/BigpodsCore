using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Services;

using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Commands;

public sealed class CreateOneWarehouseHandler(
    [Service] ICreateOneWarehouseService createOneWarehouseService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<CreateOneWarehouseCommand, WarehouseModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ICreateOneWarehouseService _createOneWarehouseService = createOneWarehouseService;

    public async Task<WarehouseModel> Handle(
        CreateOneWarehouseCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var fetchResponse = await _createOneWarehouseService.ExecuteAsync(command: command, cancellationToken: cancellationToken);

        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();

        var aggregateRoot = WarehouseAggregateRoot.CreateOne(
            warehouse: command.WarehouseDto,
            data: fetchResponse
        );

        var warehouseModel = _mapper.Map<WarehouseModel>(source: aggregateRoot);

        await warehousesRepository.CreateOneAsync(entity: warehouseModel, cancellationToken: cancellationToken);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return warehouseModel;
    }
}

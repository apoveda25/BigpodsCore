using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Services;

using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Commands;

public sealed class UpdateOneWarehouseHandler(
    [Service] IUpdateOneWarehouseService updateOneWarehouseService,
    [Service] IUnitOfWork unitOfWork,
    [Service] IMapper mapper
) : IRequestHandler<UpdateOneWarehouseCommand, WarehouseModel>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IUpdateOneWarehouseService _updateOneWarehouseService = updateOneWarehouseService;

    public async Task<WarehouseModel> Handle(
        UpdateOneWarehouseCommand request,
        CancellationToken cancellationToken
    )
    {
        var fetchResponse = await _updateOneWarehouseService.ExecuteAsync(command: request, cancellationToken: cancellationToken);

        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();

        var aggregateRoot = WarehouseAggregateRoot.UpdateOne(
            warehouse: request.WarehouseDto,
            warehouseFoundById: fetchResponse.WarehouseFoundById,
            warehouseFoundByName: fetchResponse.WarehouseFoundByName
        );

        var warehouseModel = _mapper.Map<WarehouseModel>(source: aggregateRoot);

        warehousesRepository.UpdateOne(entity: warehouseModel);

        await _unitOfWork.CompleteAsync(cancellationToken: cancellationToken);

        return warehouseModel;
    }
}
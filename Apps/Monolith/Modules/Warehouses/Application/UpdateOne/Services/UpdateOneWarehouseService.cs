using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Services;

namespace Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Services;

public sealed class UpdateOneWarehouseService([Service] IUnitOfWork unitOfWork)
    : IUpdateOneWarehouseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<IUpdateOneWarehouseServiceResponse> ExecuteAsync(
        IUpdateOneWarehouseCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();

        var warehouseFoundById = await warehousesRepository.FindOneAsync(
            filter: x => x.Id == command.WarehouseDto.Id,
            cancellationToken: cancellationToken
        );
        var warehouseFoundByName = command.WarehouseDto.Name is null
            ? null
            : await warehousesRepository.FindOneAsync(
                filter: x => x.Name == command.WarehouseDto.Name,
                cancellationToken: cancellationToken
            );

        return new UpdateOneWarehouseServiceResponse(
            WarehouseFoundById: warehouseFoundById,
            WarehouseFoundByName: warehouseFoundByName
        );
    }
}

public sealed record UpdateOneWarehouseServiceResponse(
    IWarehouseModel? WarehouseFoundById,
    IWarehouseModel? WarehouseFoundByName
) : IUpdateOneWarehouseServiceResponse;

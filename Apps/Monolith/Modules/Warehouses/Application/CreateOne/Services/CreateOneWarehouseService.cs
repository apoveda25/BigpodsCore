using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Services;

namespace Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Services;

public sealed class CreateOneWarehouseService(
    [Service] IUnitOfWork unitOfWork
) : ICreateOneWarehouseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ICreateOneWarehouseServiceResponse> ExecuteAsync(
        ICreateOneWarehouseCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var warehousesRepository = _unitOfWork.GetRepository<WarehouseModel>();

        var warehouseFoundById = await warehousesRepository.FindOneAsync(
            filter: x => x.Id == command.WarehouseDto.Id,
            cancellationToken: cancellationToken
        );
        var warehouseFoundByName = await warehousesRepository.FindOneAsync(
            filter: x => x.Name == command.WarehouseDto.Name,
            cancellationToken: cancellationToken
        );

        return new CreateOneWarehouseServiceResponse(
            WarehouseFoundById: warehouseFoundById,
            WarehouseFoundByName: warehouseFoundByName
        );
    }
}

public sealed record CreateOneWarehouseServiceResponse(
    IWarehouseModel? WarehouseFoundById,
    IWarehouseModel? WarehouseFoundByName
) : ICreateOneWarehouseServiceResponse;
using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Application.Common.Policies;
using Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Inputs;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Infrastructure.UpdateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class UpdateOneWarehouseMutation
{
    [Authorize(Policy = WarehousesPolicies.UpdateOneWarehousesPolicy)]
    public async Task<WarehouseModel> UpdateOneWarehouse(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        UpdateOneWarehouseInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var warehouseDto = mapper.Map<UpdateOneWarehouseDto>(
            source: input with
            {
                UpdatedBy = userId
            }
        );

        return await mediator.Send(new UpdateOneWarehouseCommand(WarehouseDto: warehouseDto));
    }
}

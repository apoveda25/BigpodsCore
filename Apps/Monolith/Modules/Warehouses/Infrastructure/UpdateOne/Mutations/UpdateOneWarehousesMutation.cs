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
public sealed class UpdateOneWarehousesMutation
{
    [Authorize(Policy = WarehousesPolicies.UpdateOneWarehousesPolicy)]
    public async Task<WarehouseModel> UpdateOneWarehouses(
            [Service] IMediator mediator,
            [Service] IMapper mapper,
            ClaimsPrincipal claimsPrincipal,
            UpdateOneWarehouseInput input
        )
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        var warehouseDto = mapper.Map<UpdateOneWarehouseDto>(source: input with { UpdatedBy = userId });

        return await mediator.Send(
            new UpdateOneWarehouseCommand(
                WarehouseDto: warehouseDto
            )
        );
    }
}

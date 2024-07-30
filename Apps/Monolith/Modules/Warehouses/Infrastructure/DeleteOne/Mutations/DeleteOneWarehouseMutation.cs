using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Application.Common.Policies;
using Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Inputs;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Infrastructure.DeleteOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class DeleteOneWarehouseMutation
{
    [Authorize(Policy = WarehousesPolicies.DeleteOneWarehousesPolicy)]
    public async Task<WarehouseModel> DeleteOneWarehouse(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        DeleteOneWarehouseInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var warehouseDto = mapper.Map<DeleteOneWarehouseDto>(
            source: input with
            {
                DeletedBy = userId
            }
        );

        return await mediator.Send(new DeleteOneWarehouseCommand(WarehouseDto: warehouseDto));
    }
}

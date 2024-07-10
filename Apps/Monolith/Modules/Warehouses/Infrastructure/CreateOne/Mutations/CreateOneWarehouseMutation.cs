using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Application.Common.Policies;
using Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Inputs;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Warehouses.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneWarehouseMutation
{
    [Authorize(Policy = WarehousesPolicies.CreateOneWarehousesPolicy)]
    public async Task<WarehouseModel> CreateOneWarehouse(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneWarehouseInput input
    )
    {
        var userId = Guid.Parse(
            claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString()
        );

        var warehouseDto = mapper.Map<CreateOneWarehouseDto>(
            source: input with
            {
                CreatedBy = userId
            }
        );

        return await mediator.Send(new CreateOneWarehouseCommand(WarehouseDto: warehouseDto));
    }
}

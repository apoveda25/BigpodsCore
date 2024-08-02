using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
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
    [Error(typeof(NotFoundException))]
    [Error(typeof(ConflictException))]
    [Authorize(Policy = WarehousesPolicies.CreateOneWarehousesPolicy)]
    public async Task<WarehouseModel> CreateOneWarehouse(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneWarehouseInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var warehouseDto = mapper.Map<CreateOneWarehouseDto>(
            source: input with
            {
                CreatedBy = userId
            }
        );

        return await mediator.Send(new CreateOneWarehouseCommand(WarehouseDto: warehouseDto));
    }
}

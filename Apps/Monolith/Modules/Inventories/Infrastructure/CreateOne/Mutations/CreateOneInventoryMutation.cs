using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Inventories.Application.Common.Policies;
using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Inventories.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneInventoryMutation
{
    [Error(typeof(NotFoundException))]
    [Error(typeof(ConflictException))]
    [Authorize(Policy = InventoriesPolicies.CreateOneInventoriesPolicy)]
    public async Task<InventoryModel> CreateOneInventory(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneInventoryInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var inventoryDto = mapper.Map<CreateOneInventoryDto>(
            source: input with
            {
                CreatedBy = userId
            }
        );

        return await mediator.Send(new CreateOneInventoryCommand(InventoryDto: inventoryDto));
    }
}

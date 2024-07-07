using System.Security.Claims;

using AutoMapper;

using Bigpods.Monolith.Modules.Inventories.Application.Common.Policies;
using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;

using MediatR;

namespace Bigpods.Monolith.Modules.Inventories.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneInventoryMutation
{
    [Authorize(Policy = InventoriesPolicies.CreateOneInventoriesPolicy)]
    public async Task<InventoryModel> CreateOneInventory(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneInventoryInput input
    )
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        var inventoryDto = mapper.Map<CreateOneInventoryDto>(source: input with { CreatedBy = userId });

        return await mediator.Send(
            new CreateOneInventoryCommand(
                InventoryDto: inventoryDto
            )
        );
    }
}

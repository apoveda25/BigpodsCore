using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Inventories.Application.Common.Policies;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Inventories.Infrastructure.DeleteOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class DeleteOneInventoryMutation
{
    [Authorize(Policy = InventoriesPolicies.DeleteOneInventoriesPolicy)]
    public async Task<InventoryModel> DeleteOneInventory(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        DeleteOneInventoryInput input
    )
    {
        var userId = Guid.Parse(
            claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString()
        );

        var inventoryDto = mapper.Map<DeleteOneInventoryDto>(
            source: input with
            {
                DeletedBy = userId
            }
        );

        return await mediator.Send(new DeleteOneInventoryCommand(InventoryDto: inventoryDto));
    }
}

using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Inventories.Application.Common.Policies;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Inventories.Infrastructure.DeleteOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class DeleteOneInventoryMutation
{
    [Error(typeof(NotFoundException))]
    [Error(typeof(ConflictException))]
    [Authorize(Policy = InventoriesPolicies.DeleteOneInventoriesPolicy)]
    public async Task<InventoryModel> DeleteOneInventory(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        DeleteOneInventoryInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var inventoryDto = mapper.Map<DeleteOneInventoryDto>(
            source: input with
            {
                DeletedBy = userId
            }
        );

        return await mediator.Send(new DeleteOneInventoryCommand(InventoryDto: inventoryDto));
    }
}

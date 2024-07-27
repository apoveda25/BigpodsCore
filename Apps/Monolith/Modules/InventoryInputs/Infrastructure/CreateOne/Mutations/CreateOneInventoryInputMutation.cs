using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.InventoryInputs.Application.Common.Policies;
using Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.InventoryInputs.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneInventoryInputMutation
{
    [Authorize(Policy = InventoryInputsPolicies.CreateOneInventoryInputsPolicy)]
    public async Task<InventoryInputModel> CreateOneInventoryInput(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneInventoryInputInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var inventoryInputDto = mapper.Map<CreateOneInventoryInputDto>(
            source: input with
            {
                CreatedBy = userId
            }
        );

        return await mediator.Send(
            new CreateOneInventoryInputCommand(InventoryInputDto: inventoryInputDto)
        );
    }
}

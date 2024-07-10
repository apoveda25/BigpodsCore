using System.Security.Claims;

using AutoMapper;

using Bigpods.Monolith.Modules.InventoryOutputs.Application.Common.Policies;
using Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;

using MediatR;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneInventoryOutputMutation
{
    [Authorize(Policy = InventoryOutputsPolicies.CreateOneInventoryOutputsPolicy)]
    public async Task<InventoryOutputModel> CreateOneInventoryOutput(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneInventoryOutputInput input
    )
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        var inventoryInputDto = mapper.Map<CreateOneInventoryOutputDto>(source: input with { CreatedBy = userId });

        return await mediator.Send(new CreateOneInventoryOutputCommand(InventoryOutputDto: inventoryInputDto));
    }
}

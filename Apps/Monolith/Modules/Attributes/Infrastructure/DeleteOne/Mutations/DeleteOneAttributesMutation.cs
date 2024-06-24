using System.Security.Claims;

using AutoMapper;

using Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;

using MediatR;

namespace Bigpods.Monolith.Modules.Attributes.Infrastructure.DeleteOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class DeleteOneAttributesMutation
{
    [Authorize(Policy = AttributesPolicies.DeleteOneAttributesPolicy)]
    public async Task<AttributeModel> DeleteOneAttributes(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        DeleteOneAttributeInput input
    )
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        var dto = mapper.Map<DeleteOneAttributeDto>(source: input with { DeletedBy = userId });

        return await mediator.Send(
            new DeleteOneAttributeCommand(
                AttributeDto: dto
            )
        );
    }
}

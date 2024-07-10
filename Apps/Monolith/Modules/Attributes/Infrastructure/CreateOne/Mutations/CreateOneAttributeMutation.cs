using System.Security.Claims;

using AutoMapper;

using Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;

using MediatR;

namespace Bigpods.Monolith.Modules.Attributes.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneAttributeMutation
{
    [Authorize(Policy = AttributesPolicies.CreateOneAttributesPolicy)]
    public async Task<AttributeModel> CreateOneAttribute(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneAttributeInput input
    )
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        var dto = mapper.Map<CreateOneAttributeDto>(source: input with { CreatedBy = userId });

        return await mediator.Send(
            new CreateOneAttributeCommand(
                AttributeDto: dto
            )
        );
    }
}

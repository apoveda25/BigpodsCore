using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Attributes.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneAttributeMutation
{
    [Error(typeof(ConflictException))]
    [Error(typeof(NotFoundException))]
    [Authorize(Policy = AttributesPolicies.CreateOneAttributesPolicy)]
    public async Task<AttributeModel> CreateOneAttribute(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneAttributeInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var dto = mapper.Map<CreateOneAttributeDto>(source: input with { CreatedBy = userId });

        return await mediator.Send(new CreateOneAttributeCommand(AttributeDto: dto));
    }
}

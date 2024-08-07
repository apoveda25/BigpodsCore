using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Attributes.Infrastructure.DeleteOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class DeleteOneAttributeMutation
{
    [Error(typeof(NotFoundException))]
    [Error(typeof(ConflictException))]
    [Authorize(Policy = AttributesPolicies.DeleteOneAttributesPolicy)]
    public async Task<AttributeModel> DeleteOneAttribute(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        DeleteOneAttributeInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var dto = mapper.Map<DeleteOneAttributeDto>(source: input with { DeletedBy = userId });

        return await mediator.Send(new DeleteOneAttributeCommand(AttributeDto: dto));
    }
}

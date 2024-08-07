using System.Security.Claims;
using System.Text;
using AutoMapper;
using Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Policies;
using Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneAttributeTypeMutation
{
    [Error(typeof(NotFoundException))]
    [Error(typeof(ConflictException))]
    [Authorize(Policy = AttributeTypesPolicies.CreateOneAttributeTypesPolicy)]
    public async Task<AttributeTypeModel> CreateOneAttributeType(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneAttributeTypeInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var attributeTypeDto = mapper.Map<CreateOneAttributeTypeDto>(
            source: input with
            {
                ValuePattern = Encoding.UTF8.GetString(
                    Convert.FromBase64String(input.ValuePattern)
                ),
                MeasuringUnitPattern = Encoding.UTF8.GetString(
                    Convert.FromBase64String(input.MeasuringUnitPattern)
                ),
                CreatedBy = userId
            }
        );

        return await mediator.Send(new CreateOneAttributeTypeCommand(attributeTypeDto));
    }
}

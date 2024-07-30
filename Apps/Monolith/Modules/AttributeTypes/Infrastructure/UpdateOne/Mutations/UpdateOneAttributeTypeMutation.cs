using System.Security.Claims;
using System.Text;
using AutoMapper;
using Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Policies;
using Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Commands;
using Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.UpdateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class UpdateOneAttributeTypeMutation
{
    [Authorize(Policy = AttributeTypesPolicies.UpdateOneAttributeTypesPolicy)]
    public async Task<AttributeTypeModel> UpdateOneAttributeType(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        UpdateOneAttributeTypeInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var attributeTypeDto = mapper.Map<UpdateOneAttributeTypeDto>(
            source: input with
            {
                ValuePattern = input.ValuePattern is null
                    ? null
                    : Encoding.UTF8.GetString(Convert.FromBase64String(input.ValuePattern)),
                MeasuringUnitPattern = input.MeasuringUnitPattern is null
                    ? null
                    : Encoding.UTF8.GetString(Convert.FromBase64String(input.MeasuringUnitPattern)),
                UpdatedBy = userId
            }
        );

        return await mediator.Send(new UpdateOneAttributeTypeCommand(attributeTypeDto));
    }
}

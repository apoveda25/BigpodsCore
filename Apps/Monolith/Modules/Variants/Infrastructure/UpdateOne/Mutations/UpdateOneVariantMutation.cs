using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Application.Common.Policies;
using Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Inputs;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Infrastructure.UpdateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class UpdateOneVariantMutation
{
    [Authorize(Policy = VariantsPolicies.UpdateOneVariantsPolicy)]
    public async Task<VariantModel> UpdateOneVariant(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        UpdateOneVariantInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var variantDto = mapper.Map<UpdateOneVariantDto>(source: input with { UpdatedBy = userId });

        return await mediator.Send(new UpdateOneVariantCommand(VariantDto: variantDto));
    }
}

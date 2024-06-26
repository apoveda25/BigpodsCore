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
public sealed class UpdateOneVariantsMutation
{
    [Authorize(Policy = VariantsPolicies.UpdateOneVariantsPolicy)]
    public async Task<VariantModel> UpdateOneVariants(
                [Service] IMediator mediator,
                [Service] IMapper mapper,
                ClaimsPrincipal claimsPrincipal,
                UpdateOneVariantInput input
           )
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        var variantDto = mapper.Map<UpdateOneVariantDto>(source: input with { UpdatedBy = userId });

        return await mediator.Send(
            new UpdateOneVariantCommand(
                VariantDto: variantDto
            )
        );
    }
}

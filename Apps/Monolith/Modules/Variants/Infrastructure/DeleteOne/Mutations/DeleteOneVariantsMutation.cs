using System.Security.Claims;

using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Application.Common.Policies;
using Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Inputs;

using HotChocolate.Authorization;

using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Infrastructure.DeleteOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class DeleteOneVariantsMutation
{
    [Authorize(Policy = VariantsPolicies.DeleteOneVariantsPolicy)]
    public async Task<VariantModel> DeleteOneVariants(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        DeleteOneVariantInput input
    )
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        var variantDto = mapper.Map<DeleteOneVariantDto>(source: input with { DeletedBy = userId });

        return await mediator.Send(
            new DeleteOneVariantCommand(
                VariantDto: variantDto
            )
        );
    }
}

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
public sealed class DeleteOneVariantMutation
{
    [Authorize(Policy = VariantsPolicies.DeleteOneVariantsPolicy)]
    public async Task<VariantModel> DeleteOneVariant(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        DeleteOneVariantInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var variantDto = mapper.Map<DeleteOneVariantDto>(source: input with { DeletedBy = userId });

        return await mediator.Send(new DeleteOneVariantCommand(VariantDto: variantDto));
    }
}

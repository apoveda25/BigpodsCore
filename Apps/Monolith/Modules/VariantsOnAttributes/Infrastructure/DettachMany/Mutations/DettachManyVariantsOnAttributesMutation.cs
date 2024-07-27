using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Commands;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Dtos;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Inputs;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Infrastructure.DettachMany.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class DettachManyVariantsOnAttributesMutation
{
    [Authorize(Policy = VariantsOnAttributesPolicies.DettachManyVariantsOnAttributesPolicy)]
    public async Task<VariantOnAttributeModel[]> DettachManyVariantsOnAttributes(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        DettachManyVariantOnAttributeInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        input.VariantsOnAttributes = input
            .VariantsOnAttributes.Select(x => x with { DeletedBy = userId })
            .ToArray();

        var productDto = mapper.Map<DettachManyVariantOnAttributeDto>(source: input);
        var variantOnAttributeDtos = mapper.Map<DeleteOneVariantOnAttributeDto[]>(
            source: input.VariantsOnAttributes
        );

        return await mediator.Send(
            new DettachManyVariantOnAttributeCommand(
                ProductDto: productDto,
                VariantOnAttributeDtos: variantOnAttributeDtos
            )
        );
    }
}

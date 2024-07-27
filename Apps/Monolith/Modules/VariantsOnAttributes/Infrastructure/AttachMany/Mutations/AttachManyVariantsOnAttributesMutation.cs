using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Commands;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Dtos;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Inputs;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Policies;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Infrastructure.AttachMany.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class AttachManyVariantsOnAttributesMutation
{
    [Authorize(Policy = VariantsOnAttributesPolicies.AttachManyVariantsOnAttributesPolicy)]
    public async Task<VariantOnAttributeModel[]> AttachManyVariantsOnAttributes(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        AttachManyVariantOnAttributeInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        input.VariantsOnAttributes = input
            .VariantsOnAttributes.Select(x => x with { CreatedBy = userId })
            .ToArray();

        var productDto = mapper.Map<AttachManyVariantOnAttributeDto>(source: input);
        var variantOnAttributeDtos = mapper.Map<CreateOneVariantOnAttributeDto[]>(
            source: input.VariantsOnAttributes
        );

        return await mediator.Send(
            new AttachManyVariantOnAttributeCommand(
                ProductDto: productDto,
                VariantOnAttributeDtos: variantOnAttributeDtos
            )
        );
    }
}

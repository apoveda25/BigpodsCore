using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Application.Common.Policies;
using Bigpods.Monolith.Modules.Variants.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.Variants.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Application.CreateOne.Inputs;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Variants.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneVariantMutation
{
    [Error(typeof(NotFoundException))]
    [Error(typeof(ConflictException))]
    [Authorize(Policy = VariantsPolicies.CreateOneVariantsPolicy)]
    public async Task<VariantModel> CreateOneVariant(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneVariantInput input
    )
    {
        var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = sub is not null ? Guid.Parse(sub) : Guid.Empty;

        var normalizedInput = input with
        {
            CreatedBy = userId,
            VariantsOnAttributes = input
                .VariantsOnAttributes.Select(v =>
                    v with
                    {
                        CreatedBy = userId,
                        CreatedAtTimezone = input.CreatedAtTimezone,
                        VariantId = input.Id
                    }
                )
                .ToArray()
        };

        var variantDto = mapper.Map<CreateOneVariantDto>(source: normalizedInput);
        var variantOnAttributeDtos = mapper.Map<CreateOneVariantOnAttributeDto[]>(
            source: normalizedInput.VariantsOnAttributes
        );

        return await mediator.Send(
            new CreateOneVariantCommand(
                VariantDto: variantDto,
                VariantOnAttributeDtos: variantOnAttributeDtos
            )
        );
    }
}

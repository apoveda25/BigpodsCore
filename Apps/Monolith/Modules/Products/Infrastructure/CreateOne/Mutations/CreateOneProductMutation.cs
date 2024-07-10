using System.Security.Claims;
using AutoMapper;
using Bigpods.Monolith.Modules.Products.Application.Common.Policies;
using Bigpods.Monolith.Modules.Products.Application.CreateOne.Commands;
using Bigpods.Monolith.Modules.Products.Application.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Products.Application.CreateOne.Inputs;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;
using MediatR;

namespace Bigpods.Monolith.Modules.Products.Infrastructure.CreateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class CreateOneProductMutation
{
    [Authorize(Policy = ProductsPolicies.CreateOneProductsPolicy)]
    public async Task<ProductModel> CreateOneProduct(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        CreateOneProductInput input
    )
    {
        var userId = Guid.Parse(
            claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString()
        );

        var normalizedInput = input with
        {
            CreatedBy = userId,
            Variants = input
                .Variants.Select(v =>
                    v with
                    {
                        CreatedBy = userId,
                        CreatedAtTimezone = input.CreatedAtTimezone,
                        ProductId = input.Id,
                        VariantsOnAttributes = v
                            .VariantsOnAttributes.Select(voa =>
                                voa with
                                {
                                    CreatedBy = userId,
                                    CreatedAtTimezone = input.CreatedAtTimezone,
                                    VariantId = v.Id
                                }
                            )
                            .ToArray()
                    }
                )
                .ToArray()
        };

        var productDto = mapper.Map<CreateOneProductDto>(source: normalizedInput);
        var variantDtos = mapper.Map<CreateOneVariantDto[]>(source: normalizedInput.Variants);
        var variantOnAttributeDtos = mapper.Map<CreateOneVariantOnAttributeDto[]>(
            source: normalizedInput.Variants.SelectMany(v => v.VariantsOnAttributes)
        );

        return await mediator.Send(
            new CreateOneProductCommand(productDto, variantDtos, variantOnAttributeDtos)
        );
    }
}

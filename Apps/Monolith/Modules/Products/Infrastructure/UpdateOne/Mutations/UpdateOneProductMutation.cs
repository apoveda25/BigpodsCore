using System.Security.Claims;

using AutoMapper;

using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Products.Application.Common.Policies;
using Bigpods.Monolith.Modules.Products.Application.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Products.Application.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Products.Application.UpdateOne.Inputs;

using HotChocolate.Authorization;

using MediatR;

namespace Bigpods.Monolith.Modules.Products.Infrastructure.UpdateOne.Mutations;

[ExtendObjectType(name: OperationTypeNames.Mutation)]
public sealed class UpdateOneProductMutation
{
    [Authorize(Policy = ProductsPolicies.UpdateOneProductsPolicy)]
    public async Task<ProductModel> UpdateOneProduct(
        [Service] IMediator mediator,
        [Service] IMapper mapper,
        ClaimsPrincipal claimsPrincipal,
        UpdateOneProductInput input
    )
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());

        var normalizedInput = input with { UpdatedBy = userId };

        var productDto = mapper.Map<UpdateOneProductDto>(source: normalizedInput);

        return await mediator.Send(new UpdateOneProductCommand(productDto));
    }
}

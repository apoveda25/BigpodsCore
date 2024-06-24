using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Products.Application.Common.Policies;

using HotChocolate.Authorization;
using Bigpods.Monolith.Modules.Shared.Domain.Database;

namespace Bigpods.Monolith.Modules.Products.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindManyProductsQuery
{
    [Authorize(Policy = ProductsPolicies.ReadManyProductsPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ProductModel> FindManyProducts([Service] IUnitOfWork unitOfWork)
    {
        return unitOfWork.GetRepository<ProductModel>().Model;
    }
}

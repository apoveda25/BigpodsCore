using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Application.Common.Policies;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.Variants.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FinaManyVariantsQuery
{
    [Authorize(Policy = VariantsPolicies.ReadManyVariantsPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<VariantModel> FindManyVariants([Service] IUnitOfWork unitOfWork)
    {
        return unitOfWork.GetRepository<VariantModel>().Model;
    }
}

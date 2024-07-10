using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Policies;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FinaManyVariantsOnAttributesQuery
{
    [Authorize(Policy = VariantsOnAttributesPolicies.ReadManyVariantsOnAttributesPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<VariantOnAttributeModel> FindManyVariantsOnAttributes(
        [Service] IUnitOfWork unitOfWork
    )
    {
        return unitOfWork.GetRepository<VariantOnAttributeModel>().Model;
    }
}

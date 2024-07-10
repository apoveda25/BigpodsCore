using Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.Attributes.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindManyAttributesQuery
{
    [Authorize(Policy = AttributesPolicies.ReadManyAttributesPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<AttributeModel> FindManyAttributes([Service] IUnitOfWork unitOfWork)
    {
        return unitOfWork.GetRepository<AttributeModel>().Model;
    }
}

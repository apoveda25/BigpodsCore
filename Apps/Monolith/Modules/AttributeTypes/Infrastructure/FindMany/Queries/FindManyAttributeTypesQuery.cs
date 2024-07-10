using Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindManyAttributeTypesQuery
{
    [Authorize(Policy = AttributeTypesPolicies.ReadManyAttributeTypesPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<AttributeTypeModel> FindManyAttributeTypes([Service] IUnitOfWork unitOfWork)
    {
        return unitOfWork.GetRepository<AttributeTypeModel>().Model;
    }
}

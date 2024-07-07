using Bigpods.Monolith.Modules.Inventories.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.Inventories.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindManyInventoriesQuery
{
    [Authorize(Policy = InventoriesPolicies.ReadManyInventoriesPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<InventoryModel> FindManyInventories([Service] IUnitOfWork unitOfWork)
    {
        return unitOfWork.GetRepository<InventoryModel>().Model;
    }
}

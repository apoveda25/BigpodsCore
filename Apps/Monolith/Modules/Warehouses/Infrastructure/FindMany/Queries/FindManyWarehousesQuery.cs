using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Application.Common.Policies;

using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.Warehouses.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindManyWarehousesQuery
{
    [Authorize(Policy = WarehousesPolicies.ReadManyWarehousesPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<WarehouseModel> FindManyWarehouses([Service] IUnitOfWork unitOfWork)
    {
        return unitOfWork.GetRepository<WarehouseModel>().Model;
    }
}

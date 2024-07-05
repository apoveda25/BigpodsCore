using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Warehouses.Application.Common.Policies;

using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.Warehouses.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneWarehousesQuery
{
    [Authorize(Policy = WarehousesPolicies.ReadOneWarehousesPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<WarehouseModel> FindOneWarehouses([Service] IUnitOfWork unitOfWork, Guid id)
        => unitOfWork.GetRepository<WarehouseModel>().Model.Where(predicate: p => p.Id == id);
}

using Bigpods.Monolith.Modules.Inventories.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.Inventories.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneInventoryQuery
{
    [Authorize(Policy = InventoriesPolicies.ReadOneInventoriesPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<InventoryModel> FindOneInventory([Service] IUnitOfWork unitOfWork, Guid id) =>
        unitOfWork.GetRepository<InventoryModel>().Model.Where(predicate: p => p.Id == id);
}

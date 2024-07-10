using Bigpods.Monolith.Modules.InventoryOutputs.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindManyInventoryOutputsQuery
{
    [Authorize(Policy = InventoryOutputsPolicies.ReadManyInventoryOutputsPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<InventoryOutputModel> FindManyInventoryOutputs([Service] IUnitOfWork unitOfWork)
    {
        return unitOfWork.GetRepository<InventoryOutputModel>().Model;
    }
}

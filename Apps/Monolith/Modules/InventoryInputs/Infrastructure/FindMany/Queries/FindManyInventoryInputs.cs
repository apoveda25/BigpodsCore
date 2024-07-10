using Bigpods.Monolith.Modules.InventoryInputs.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.InventoryInputs.Infrastructure.FindMany.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindManyInventoryInputsQuery
{
    [Authorize(Policy = InventoryInputsPolicies.ReadManyInventoryInputsPolicy)]
    [UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<InventoryInputModel> FindManyInventoryInputs([Service] IUnitOfWork unitOfWork)
    {
        return unitOfWork.GetRepository<InventoryInputModel>().Model;
    }
}

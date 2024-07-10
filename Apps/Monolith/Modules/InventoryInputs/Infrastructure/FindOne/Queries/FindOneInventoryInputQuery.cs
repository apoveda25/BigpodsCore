using Bigpods.Monolith.Modules.InventoryInputs.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.InventoryInputs.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneInventoryInputQuery
{
    [Authorize(Policy = InventoryInputsPolicies.ReadOneInventoryInputsPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<InventoryInputModel> FindOneInventoryInput([Service] IUnitOfWork unitOfWork, Guid id)
        => unitOfWork.GetRepository<InventoryInputModel>().Model.Where(predicate: p => p.Id == id);
}

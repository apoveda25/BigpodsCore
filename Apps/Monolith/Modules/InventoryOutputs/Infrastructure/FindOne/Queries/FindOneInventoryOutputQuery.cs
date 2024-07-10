using Bigpods.Monolith.Modules.InventoryOutputs.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneInventoryOutputQuery
{
    [Authorize(Policy = InventoryOutputsPolicies.ReadOneInventoryOutputsPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<InventoryOutputModel> FindOneInventoryOutput([Service] IUnitOfWork unitOfWork, Guid id)
        => unitOfWork.GetRepository<InventoryOutputModel>().Model.Where(predicate: p => p.Id == id);
}

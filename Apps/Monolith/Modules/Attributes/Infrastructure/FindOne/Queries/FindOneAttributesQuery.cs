using Bigpods.Monolith.Modules.Attributes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

using HotChocolate.Authorization;
using Bigpods.Monolith.Modules.Shared.Domain.Database;

namespace Bigpods.Monolith.Modules.Attributes.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneAttributesQuery
{

    [Authorize(Policy = AttributesPolicies.ReadOneAttributesPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<AttributeModel> FindOneAttributes([Service] IUnitOfWork unitOfWork, Guid id)
        => unitOfWork.GetRepository<AttributeModel>().Model.Where(predicate: p => p.Id == id);
}

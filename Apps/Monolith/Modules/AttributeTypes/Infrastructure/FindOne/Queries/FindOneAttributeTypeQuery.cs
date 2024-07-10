using Bigpods.Monolith.Modules.AttributeTypes.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.AttributeTypes.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneAttributeTypeQuery
{
    [Authorize(Policy = AttributeTypesPolicies.ReadOneAttributeTypesPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<AttributeTypeModel> FindOneAttributeType(
        [Service] IUnitOfWork unitOfWork,
        Guid id
    )
    {
        return unitOfWork
            .GetRepository<AttributeTypeModel>()
            .Model.Where(predicate: p => p.Id == id);
    }
}

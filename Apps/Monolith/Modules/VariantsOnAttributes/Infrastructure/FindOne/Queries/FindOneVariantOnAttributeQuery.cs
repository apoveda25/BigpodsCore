using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Application.Common.Policies;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneVariantOnAttributeQuery
{
    [Authorize(Policy = VariantsOnAttributesPolicies.ReadOneVariantsOnAttributesPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<VariantOnAttributeModel> FindOneVariantOnAttribute(
        [Service] IUnitOfWork unitOfWork,
        Guid id
    ) =>
        unitOfWork.GetRepository<VariantOnAttributeModel>().Model.Where(predicate: p => p.Id == id);
}

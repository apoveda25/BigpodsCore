using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using Bigpods.Monolith.Modules.Variants.Application.Common.Policies;

using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.Variants.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneVariantsQuery
{
    [Authorize(Policy = VariantsPolicies.ReadOneVariantsPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<VariantModel> FindOneVariants([Service] IUnitOfWork unitOfWork, Guid id)
        => unitOfWork.GetRepository<VariantModel>().Model.Where(predicate: p => p.Id == id);
}

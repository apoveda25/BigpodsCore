using Bigpods.Monolith.Modules.Products.Application.Common.Policies;
using Bigpods.Monolith.Modules.Shared.Domain.Database;
using Bigpods.Monolith.Modules.Shared.Infrastructure.Models;
using HotChocolate.Authorization;

namespace Bigpods.Monolith.Modules.Products.Infrastructure.FindOne.Queries;

[ExtendObjectType(name: OperationTypeNames.Query)]
public sealed class FindOneProductQuery
{
    [Authorize(Policy = ProductsPolicies.ReadOneProductsPolicy)]
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<ProductModel> FindOneProduct([Service] IUnitOfWork unitOfWork, Guid id) =>
        unitOfWork.GetRepository<ProductModel>().Model.Where(predicate: p => p.Id == id);
}

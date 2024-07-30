using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Aggregates;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Factories;

public static class ProductAggregateRootFactory
{
    public static ProductAggregateRoot BuildOne(IProductModel? product)
    {
        if (product is null)
        {
            throw new NotFoundException("Product not found");
        }

        if (product.IsDeleted)
        {
            throw new NotFoundException("Product is deleted");
        }

        return new ProductAggregateRoot(
            id: product.Id,
            name: product.Name,
            description: product.Description,
            brand: product.Brand,
            model: product.Model,
            isCompleted: product.IsCompleted,
            isPublished: product.IsPublished,
            isDeleted: product.IsDeleted,
            createdAtDatetime: product.CreatedAtDatetime,
            updatedAtDatetime: product.UpdatedAtDatetime,
            deletedAtDatetime: product.DeletedAtDatetime,
            createdAtTimezone: product.CreatedAtTimezone,
            updatedAtTimezone: product.UpdatedAtTimezone,
            deletedAtTimezone: product.DeletedAtTimezone,
            createdBy: product.CreatedBy,
            updatedBy: product.UpdatedBy,
            deletedBy: product.DeletedBy
        );
    }
}

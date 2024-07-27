using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Aggregates;

namespace Bigpods.Monolith.Modules.Variants.Domain.Common.Factories;

public static class ProductAggregateRootFactory
{
    public static ProductAggregateRoot BuildOne(IProductModel? productFoundById)
    {
        if (productFoundById is null)
        {
            throw new NotFoundException("Product does not exist with this id");
        }

        if (productFoundById.IsDeleted)
        {
            throw new ConflictException("Product is deleted");
        }

        return new ProductAggregateRoot(
            id: productFoundById.Id,
            name: productFoundById.Name,
            description: productFoundById.Description,
            brand: productFoundById.Brand,
            model: productFoundById.Model,
            isCompleted: productFoundById.IsCompleted,
            isPublished: productFoundById.IsPublished,
            isDeleted: productFoundById.IsDeleted,
            createdAtDatetime: productFoundById.CreatedAtDatetime,
            updatedAtDatetime: productFoundById.UpdatedAtDatetime,
            deletedAtDatetime: productFoundById.DeletedAtDatetime,
            createdAtTimezone: productFoundById.CreatedAtTimezone,
            updatedAtTimezone: productFoundById.UpdatedAtTimezone,
            deletedAtTimezone: productFoundById.DeletedAtTimezone,
            createdBy: productFoundById.CreatedBy,
            updatedBy: productFoundById.UpdatedBy,
            deletedBy: productFoundById.DeletedBy
        );
    }
}

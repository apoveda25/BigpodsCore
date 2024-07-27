using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Entities;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Factories;

public static class VariantEntityFactory
{
    public static VariantEntity[] BuildMany(IVariantModel[] variants)
    {
        return variants.AsParallel().Select(BuildOne).ToArray();
    }

    public static VariantEntity BuildOne(IVariantModel? variant)
    {
        if (variant is null)
        {
            throw new NotFoundException("Variant not found");
        }

        if (variant.IsDeleted)
        {
            throw new NotFoundException("Variant is deleted");
        }

        return new VariantEntity(
            id: variant.Id,
            name: variant.Name,
            sku: variant.Sku,
            price: variant.Price,
            cost: variant.Cost,
            isDeleted: variant.IsDeleted,
            createdAtDatetime: variant.CreatedAtDatetime,
            updatedAtDatetime: variant.UpdatedAtDatetime,
            deletedAtDatetime: variant.DeletedAtDatetime,
            createdAtTimezone: variant.CreatedAtTimezone,
            updatedAtTimezone: variant.UpdatedAtTimezone,
            deletedAtTimezone: variant.DeletedAtTimezone,
            createdBy: variant.CreatedBy,
            updatedBy: variant.UpdatedBy,
            deletedBy: variant.DeletedBy,
            productId: variant.ProductId
        );
    }
}

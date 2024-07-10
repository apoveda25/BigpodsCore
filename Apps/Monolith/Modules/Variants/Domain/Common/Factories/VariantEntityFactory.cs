using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Domain.Common.Factories;

public sealed class VariantEntityFactory
{
    public static VariantEntity CreateOne(
        ICreateOneVariantDto variant,
        IVariantModel? variantFoundById
    )
    {
        if (variantFoundById is not null)
        {
            throw new ConflictException("Variant exist with this id");
        }

        var variantEntity = new VariantEntity(
            id: variant.Id,
            name: variant.Name,
            sku: string.Empty,
            price: variant.Price,
            cost: variant.Cost,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: variant.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: variant.CreatedBy,
            updatedBy: null,
            deletedBy: null,
            productId: variant.ProductId
        );

        if (variantEntity.Price.Value <= variantEntity.Cost.Value)
        {
            throw new ConflictException("Price must be greater than cost");
        }

        return variantEntity;
    }

    public static VariantEntity UpdateOne(
        IUpdateOneVariantDto variant,
        IVariantModel? variantFoundById
    )
    {
        if (variantFoundById is null)
        {
            throw new NotFoundException("Variant not exist with this id");
        }

        if (variantFoundById.IsDeleted)
        {
            throw new ConflictException("Variant is deleted");
        }

        if (variantFoundById.Id != variant.Id)
        {
            throw new ConflictException("Variant id does not match");
        }

        var entity = new VariantEntity(
            id: variantFoundById.Id,
            name: variant.Name ?? variantFoundById.Name,
            sku: variantFoundById.Sku,
            price: variant.Price ?? variantFoundById.Price,
            cost: variant.Cost ?? variantFoundById.Cost,
            isDeleted: variantFoundById.IsDeleted,
            createdAtDatetime: variantFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: variantFoundById.DeletedAtDatetime,
            createdAtTimezone: variantFoundById.CreatedAtTimezone,
            updatedAtTimezone: variant.UpdatedAtTimezone,
            deletedAtTimezone: variantFoundById.DeletedAtTimezone,
            createdBy: variantFoundById.CreatedBy,
            updatedBy: variant.UpdatedBy,
            deletedBy: variantFoundById.DeletedBy,
            productId: variantFoundById.ProductId
        );

        if (entity.Price.Value <= entity.Cost.Value)
        {
            throw new ConflictException("Price must be greater than cost");
        }

        return entity;
    }

    public static VariantEntity DeleteOne(
        IDeleteOneVariantDto variant,
        IVariantModel? variantFoundById,
        IInventoryModel? inventoryFoundByVariantId
    )
    {
        if (variantFoundById is null)
        {
            throw new ConflictException("Variant not exist with this id");
        }

        if (variantFoundById.IsDeleted)
        {
            throw new ConflictException("Variant is deleted");
        }

        if (variantFoundById.Id != variant.Id)
        {
            throw new ConflictException("Variant id not match");
        }

        if (inventoryFoundByVariantId is not null)
        {
            throw new ConflictException("Variant with inventory can not deleted");
        }

        return new VariantEntity(
            id: variantFoundById.Id,
            name: variantFoundById.Name,
            sku: variantFoundById.Sku,
            price: variantFoundById.Price,
            cost: variantFoundById.Cost,
            isDeleted: true,
            createdAtDatetime: variantFoundById.CreatedAtDatetime,
            updatedAtDatetime: variantFoundById.UpdatedAtDatetime,
            deletedAtDatetime: DateTime.Now,
            createdAtTimezone: variantFoundById.CreatedAtTimezone,
            updatedAtTimezone: variantFoundById.UpdatedAtTimezone,
            deletedAtTimezone: variant.DeletedAtTimezone,
            createdBy: variantFoundById.CreatedBy,
            updatedBy: variantFoundById.UpdatedBy,
            deletedBy: variant.DeletedBy,
            productId: variantFoundById.ProductId
        );
    }
}

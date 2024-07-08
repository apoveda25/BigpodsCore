using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;

public sealed class VariantEntity
{
    public Guid Id { get; private set; }
    public NameVO Name { get; private set; }
    public string Sku { get; private set; }
    public PriceVO Price { get; private set; }
    public CostVO Cost { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAtDatetime { get; private set; }
    public DateTime? UpdatedAtDatetime { get; private set; }
    public DateTime? DeletedAtDatetime { get; private set; }
    public string CreatedAtTimezone { get; private set; }
    public string? UpdatedAtTimezone { get; private set; }
    public string? DeletedAtTimezone { get; private set; }
    public Guid CreatedBy { get; private set; }
    public Guid? UpdatedBy { get; private set; }
    public Guid? DeletedBy { get; private set; }
    public Guid ProductId { get; private set; }
    public VariantOnAttributeEntity[] VariantsOnAttributes { get; private set; }

    private VariantEntity(
        Guid id,
        string name,
        string sku,
        decimal price,
        decimal cost,
        bool isDeleted,
        DateTime createdAtDatetime,
        DateTime? updatedAtDatetime,
        DateTime? deletedAtDatetime,
        string createdAtTimezone,
        string? updatedAtTimezone,
        string? deletedAtTimezone,
        Guid createdBy,
        Guid? updatedBy,
        Guid? deletedBy,
        Guid productId
    )
    {
        Id = id;
        Name = new NameVO(name);
        Sku = sku;
        Price = new PriceVO(price);
        Cost = new CostVO(cost);
        IsDeleted = isDeleted;
        CreatedAtDatetime = createdAtDatetime;
        UpdatedAtDatetime = updatedAtDatetime;
        DeletedAtDatetime = deletedAtDatetime;
        CreatedAtTimezone = createdAtTimezone;
        UpdatedAtTimezone = updatedAtTimezone;
        DeletedAtTimezone = deletedAtTimezone;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        DeletedBy = deletedBy;
        ProductId = productId;
        VariantsOnAttributes = [];
    }

    public static VariantEntity[] BuildMany(
        IVariantModel[] variants
    )
    {
        return variants.Select(BuildOne).ToArray();
    }

    public static VariantEntity BuildOne(
        IVariantModel variant
    )
    {
        return new VariantEntity
        (
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

    public static VariantEntity CreateOne(
        ICreateOneVariantDto variant,
        ICreateOneVariantOnAttributeDto[] variantsOnAttributes,
        IVariantModel? variantFoundById,
        IVariantOnAttributeModel[] variantsOnAttributesFoundById,
        IAttributeModel[] attributesFoundById
    )
    {
        if (variantFoundById is not null)
        {
            throw new ConflictException("Variant exist with this id");
        }

        var variantEntity = new VariantEntity
        (
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

        var variantOnAttributeEntities = VariantOnAttributeEntity.CreateMany(
            variantsOnAttributes: variantsOnAttributes,
            variantsOnAttributesFoundById: variantsOnAttributesFoundById,
            attributesFoundById: attributesFoundById
        );

        variantEntity.AttachManyVariantOnAttribute(variantOnAttributeEntities);

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

        var entity = new VariantEntity
        (
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
        IInventoryModel? inventoryFoundByVariantId,
        IVariantOnAttributeModel[] variantsOnAttributesFoundByVariantId
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

        var variantEntity = new VariantEntity
        (
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

        variantEntity.AttachManyVariantOnAttribute(
            VariantOnAttributeEntity.BuildMany(
                variantsOnAttributes: variantsOnAttributesFoundByVariantId
            )
        );

        var variantOnAttributeEntities = VariantOnAttributeEntity.DeleteMany(
            variant: variant,
            variantsOnAttributesFoundByVariantId: variantsOnAttributesFoundByVariantId
        );

        variantEntity.DettachManyVariantOnAttribute(variantOnAttributeEntities);

        return variantEntity;
    }

    private void AttachManyVariantOnAttribute(VariantOnAttributeEntity[] variantsOnAttributes)
    {
        foreach (var variantOnAttribute in variantsOnAttributes) AttachOneVariantOnAttribute(variantOnAttribute);
    }

    private void AttachOneVariantOnAttribute(VariantOnAttributeEntity variantOnAttribute)
    {
        if (IsVariantOnAttributeExist(variantOnAttribute))
        {
            throw new ConflictException("VariantOnAttribute exist with this id or attributeId");
        }

        if (!VariantOnAttributeBelongToVariant(variantOnAttribute.VariantId))
        {
            throw new ConflictException("VariantOnAttribute not belong to this variant");
        }

        if (variantOnAttribute.IsDeleted)
        {
            throw new ConflictException("Do not attach variantOnAttribute marked as deleted");
        }

        VariantsOnAttributes = [.. VariantsOnAttributes, variantOnAttribute];
    }

    private void DettachManyVariantOnAttribute(VariantOnAttributeEntity[] variantsOnAttributes)
    {
        foreach (var variantOnAttribute in variantsOnAttributes) DettachOneVariantOnAttribute(variantOnAttribute);
    }

    private void DettachOneVariantOnAttribute(VariantOnAttributeEntity variantOnAttribute)
    {
        if (!IsVariantOnAttributeExist(variantOnAttribute))
        {
            throw new ConflictException("VariantOnAttribute not exist with this id or attributeId");
        }

        if (!VariantOnAttributeBelongToVariant(variantOnAttribute.VariantId))
        {
            throw new ConflictException("VariantOnAttribute not belong to this variant");
        }

        if (!variantOnAttribute.IsDeleted)
        {
            throw new ConflictException("Do not dettach variantOnAttribute marked as not deleted");
        }

        VariantsOnAttributes = VariantsOnAttributes.Select(x => x.Id == variantOnAttribute.Id ? variantOnAttribute : x).ToArray();
    }

    private bool IsVariantOnAttributeExist(VariantOnAttributeEntity entity)
    {
        return VariantsOnAttributes.Any(entity.IsEquals);
    }

    private bool VariantOnAttributeBelongToVariant(Guid variantId)
    {
        return variantId == Id;
    }
}

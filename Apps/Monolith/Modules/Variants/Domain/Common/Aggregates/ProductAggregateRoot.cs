using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Services;

namespace Bigpods.Monolith.Modules.Variants.Domain.Common.Aggregates;

public sealed class ProductAggregateRoot
{
    public Guid Id { get; private set; }
    public NameVO Name { get; private set; }
    public SentenceVO Description { get; private set; }
    public BrandVO Brand { get; private set; }
    public ModelVO Model { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsPublished { get; private set; }
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
    public VariantEntity[] Variants { get; private set; }

    private ProductAggregateRoot(
        Guid id,
        string name,
        string description,
        string brand,
        string model,
        bool isCompleted,
        bool isPublished,
        bool isDeleted,
        DateTime createdAtDatetime,
        DateTime? updatedAtDatetime,
        DateTime? deletedAtDatetime,
        string createdAtTimezone,
        string? updatedAtTimezone,
        string? deletedAtTimezone,
        Guid createdBy,
        Guid? updatedBy,
        Guid? deletedBy
    )
    {
        Id = id;
        Name = new NameVO(name);
        Description = new SentenceVO(description);
        Brand = new BrandVO(brand);
        Model = new ModelVO(model);
        IsCompleted = isCompleted;
        IsPublished = isPublished;
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
        Variants = [];
    }

    public static ProductAggregateRoot CreateOneVariant(
        ICreateOneVariantDto variant,
        ICreateOneVariantOnAttributeDto[] variantsOnAttributes,
        ICreateOneVariantServiceResponse data
    )
    {
        if (data.ProductFoundById is null)
        {
            throw new NotFoundException("Product does not exist with this id");
        }

        if (data.ProductFoundById.IsDeleted)
        {
            throw new ConflictException("Product is deleted");
        }

        var aggregateRoot = new ProductAggregateRoot(
            id: data.ProductFoundById.Id,
            name: data.ProductFoundById.Name,
            description: data.ProductFoundById.Description,
            brand: data.ProductFoundById.Brand,
            model: data.ProductFoundById.Model,
            isCompleted: data.ProductFoundById.IsCompleted,
            isPublished: data.ProductFoundById.IsPublished,
            isDeleted: data.ProductFoundById.IsDeleted,
            createdAtDatetime: data.ProductFoundById.CreatedAtDatetime,
            updatedAtDatetime: data.ProductFoundById.UpdatedAtDatetime,
            deletedAtDatetime: data.ProductFoundById.DeletedAtDatetime,
            createdAtTimezone: data.ProductFoundById.CreatedAtTimezone,
            updatedAtTimezone: data.ProductFoundById.UpdatedAtTimezone,
            deletedAtTimezone: data.ProductFoundById.DeletedAtTimezone,
            createdBy: data.ProductFoundById.CreatedBy,
            updatedBy: data.ProductFoundById.UpdatedBy,
            deletedBy: data.ProductFoundById.DeletedBy
        );

        var variantEntity = VariantEntity.CreateOne(
            variant: variant,
            variantsOnAttributes: variantsOnAttributes,
            variantFoundById: data.VariantFoundById,
            variantsOnAttributesFoundById: data.VariantsOnAttributesFoundById,
            attributesFoundById: data.AttributesFoundById
        );

        var variantEntities = VariantEntity.BuildMany(
            variants: data.VariantsFoundByProductId
        );

        aggregateRoot.AttachManyVariants(variantEntities);
        aggregateRoot.AttachOneVariant(variantEntity);

        return aggregateRoot;
    }

    public static ProductAggregateRoot UpdateOneVariant(
        IUpdateOneVariantDto variant,
        IUpdateOneVariantServiceResponse data
    )
    {
        if (data.ProductFoundById is null)
        {
            throw new NotFoundException("Product not exist with this id");
        }

        if (data.ProductFoundById.IsDeleted)
        {
            throw new ConflictException("Product is deleted");
        }

        var aggregateRoot = new ProductAggregateRoot(
            id: data.ProductFoundById.Id,
            name: data.ProductFoundById.Name,
            description: data.ProductFoundById.Description,
            brand: data.ProductFoundById.Brand,
            model: data.ProductFoundById.Model,
            isCompleted: data.ProductFoundById.IsCompleted,
            isPublished: data.ProductFoundById.IsPublished,
            isDeleted: data.ProductFoundById.IsDeleted,
            createdAtDatetime: data.ProductFoundById.CreatedAtDatetime,
            updatedAtDatetime: data.ProductFoundById.UpdatedAtDatetime,
            deletedAtDatetime: data.ProductFoundById.DeletedAtDatetime,
            createdAtTimezone: data.ProductFoundById.CreatedAtTimezone,
            updatedAtTimezone: data.ProductFoundById.UpdatedAtTimezone,
            deletedAtTimezone: data.ProductFoundById.DeletedAtTimezone,
            createdBy: data.ProductFoundById.CreatedBy,
            updatedBy: data.ProductFoundById.UpdatedBy,
            deletedBy: data.ProductFoundById.DeletedBy
        );

        var variantEntity = VariantEntity.UpdateOne(
            variant: variant,
            variantFoundById: data.VariantFoundById
        );

        aggregateRoot.AttachOneVariant(variantEntity);

        return aggregateRoot;
    }

    public static ProductAggregateRoot DeleteOneVariant(
        IDeleteOneVariantDto variant,
        IDeleteOneVariantServiceResponse data
    )
    {
        if (data.ProductFoundById is null)
        {
            throw new NotFoundException("Product not exist with this id");
        }

        if (data.ProductFoundById.IsDeleted)
        {
            throw new ConflictException("Do not delete product marked as deleted");
        }

        if (data.ProductFoundById.IsPublished)
        {
            throw new ConflictException("Do not delete published product");
        }

        var aggregateRoot = new ProductAggregateRoot(
            id: data.ProductFoundById.Id,
            name: data.ProductFoundById.Name,
            description: data.ProductFoundById.Description,
            brand: data.ProductFoundById.Brand,
            model: data.ProductFoundById.Model,
            isCompleted: data.ProductFoundById.IsCompleted,
            isPublished: data.ProductFoundById.IsPublished,
            isDeleted: data.ProductFoundById.IsDeleted,
            createdAtDatetime: data.ProductFoundById.CreatedAtDatetime,
            updatedAtDatetime: data.ProductFoundById.UpdatedAtDatetime,
            deletedAtDatetime: data.ProductFoundById.DeletedAtDatetime,
            createdAtTimezone: data.ProductFoundById.CreatedAtTimezone,
            updatedAtTimezone: data.ProductFoundById.UpdatedAtTimezone,
            deletedAtTimezone: data.ProductFoundById.DeletedAtTimezone,
            createdBy: data.ProductFoundById.CreatedBy,
            updatedBy: data.ProductFoundById.UpdatedBy,
            deletedBy: data.ProductFoundById.DeletedBy
        );

        var variantEntity = VariantEntity.DeleteOne(
            variant: variant,
            variantFoundById: data.VariantFoundById,
            inventoryFoundByVariantId: data.InventoryFoundByVariantId,
            variantsOnAttributesFoundByVariantId: data.VariantsOnAttributesFoundByVariantId
        );

        var variantEntities = VariantEntity.BuildMany(
            variants: data.VariantsFoundByProductId
        );

        aggregateRoot.AttachManyVariants(variantEntities);
        aggregateRoot.DettachOneVariant(variantEntity);

        return aggregateRoot;
    }

    private void AttachManyVariants(VariantEntity[] variants)
    {
        foreach (var variant in variants) AttachOneVariant(variant);
    }

    private void AttachOneVariant(VariantEntity variant)
    {
        if (IsVariantExist(variant.Id))
        {
            throw new ConflictException("Variant exist with this id");
        }

        if (!IsVariantBelongToProduct(variant.ProductId))
        {
            throw new ConflictException("Variant not belong to this product");
        }

        if (variant.IsDeleted)
        {
            throw new ConflictException("Do not attach variant marked as deleted");
        }

        Variants = [.. Variants, variant];
    }

    private void DettachOneVariant(VariantEntity variant)
    {
        if (!IsVariantExist(variant.Id))
        {
            throw new ConflictException("Variant not exist with this id");
        }

        if (!IsVariantBelongToProduct(variant.ProductId))
        {
            throw new ConflictException("Variant not belong to this product");
        }

        if (!variant.IsDeleted)
        {
            throw new ConflictException("Do not dettach variant marked as not deleted");
        }

        Variants = Variants.Select(x => x.Id == variant.Id ? variant : x).ToArray();
    }

    private bool IsVariantExist(Guid variantId)
    {
        return Variants.Any(variant => variant.Id == variantId);
    }

    private bool IsVariantBelongToProduct(Guid productId)
    {
        return productId == Id;
    }
}

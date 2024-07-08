using Bigpods.Monolith.Modules.Products.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Products.Domain.Common.Aggregates;

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

    public static ProductAggregateRoot CreateOne(
        ICreateOneProductDto product,
        ICreateOneVariantDto[] variants,
        ICreateOneVariantOnAttributeDto[] variantsOnAttributes,
        ICreateOneProductServiceResponse data
    )
    {
        if (data.ProductFoundById is not null)
        {
            throw new ConflictException("Product exist with this id");
        }

        var productAggregateRoot = new ProductAggregateRoot(
            id: product.Id,
            name: product.Name,
            description: product.Description,
            brand: product.Brand,
            model: product.Model,
            isCompleted: false,
            isPublished: false,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: product.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: product.CreatedBy,
            updatedBy: null,
            deletedBy: null
        );

        var variantEntities = VariantEntity.CreateMany(
            variants: variants,
            variantsOnAttributes: variantsOnAttributes,
            variantsFoundById: data.VariantsFoundById,
            variantsOnAttributesFoundById: data.VariantsOnAttributesFoundById,
            attributesFoundById: data.AttributesFoundById
        );

        productAggregateRoot.AttachManyVariants(variantEntities);

        return productAggregateRoot;
    }

    public static ProductAggregateRoot UpdateOne(
        IUpdateOneProductDto product,
        IUpdateOneProductServiceResponse data
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

        if (data.ProductFoundById.Id != product.Id)
        {
            throw new ConflictException("Product id does not match");
        }

        return new ProductAggregateRoot(
            id: data.ProductFoundById.Id,
            name: product.Name ?? data.ProductFoundById.Name,
            description: product.Description ?? data.ProductFoundById.Description,
            brand: product.Brand ?? data.ProductFoundById.Brand,
            model: product.Model ?? data.ProductFoundById.Model,
            isCompleted: data.ProductFoundById.IsCompleted,
            isPublished: data.ProductFoundById.IsPublished,
            isDeleted: data.ProductFoundById.IsDeleted,
            createdAtDatetime: data.ProductFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: data.ProductFoundById.DeletedAtDatetime,
            createdAtTimezone: data.ProductFoundById.CreatedAtTimezone,
            updatedAtTimezone: product.UpdatedAtTimezone,
            deletedAtTimezone: data.ProductFoundById.DeletedAtTimezone,
            createdBy: data.ProductFoundById.CreatedBy,
            updatedBy: product.UpdatedBy,
            deletedBy: data.ProductFoundById.DeletedBy
        );
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

        if (!VariantBelongToProduct(variant.ProductId))
        {
            throw new ConflictException("Variant not belong to this product");
        }

        Variants = [.. Variants, variant];
    }

    private bool IsVariantExist(Guid variantId)
    {
        return Variants.Any(variant => variant.Id == variantId);
    }

    private bool VariantBelongToProduct(Guid productId)
    {
        return productId == Id;
    }
}

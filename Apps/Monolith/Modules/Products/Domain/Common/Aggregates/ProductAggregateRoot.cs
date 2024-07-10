using Bigpods.Monolith.Modules.Products.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Products.Domain.Common.Factories;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Products.Domain.Common.Aggregates;

public sealed class ProductAggregateRoot(
    Guid? id = null,
    string? name = null,
    string? description = null,
    string? brand = null,
    string? model = null,
    bool? isCompleted = null,
    bool? isPublished = null,
    bool? isDeleted = null,
    DateTime? createdAtDatetime = null,
    DateTime? updatedAtDatetime = null,
    DateTime? deletedAtDatetime = null,
    string? createdAtTimezone = null,
    string? updatedAtTimezone = null,
    string? deletedAtTimezone = null,
    Guid? createdBy = null,
    Guid? updatedBy = null,
    Guid? deletedBy = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public NameVO Name { get; private set; } = new NameVO(name ?? string.Empty);
    public SentenceVO Description { get; private set; } =
        new SentenceVO(description ?? string.Empty);
    public BrandVO Brand { get; private set; } = new BrandVO(brand ?? string.Empty);
    public ModelVO Model { get; private set; } = new ModelVO(model ?? string.Empty);
    public bool IsCompleted { get; private set; } = isCompleted ?? false;
    public bool IsPublished { get; private set; } = isPublished ?? false;
    public bool IsDeleted { get; private set; } = isDeleted ?? false;
    public DateTime CreatedAtDatetime { get; private set; } = createdAtDatetime ?? DateTime.Now;
    public DateTime? UpdatedAtDatetime { get; private set; } = updatedAtDatetime;
    public DateTime? DeletedAtDatetime { get; private set; } = deletedAtDatetime;
    public string CreatedAtTimezone { get; private set; } = createdAtTimezone ?? string.Empty;
    public string? UpdatedAtTimezone { get; private set; } = updatedAtTimezone;
    public string? DeletedAtTimezone { get; private set; } = deletedAtTimezone;
    public Guid CreatedBy { get; private set; } = createdBy ?? Guid.Empty;
    public Guid? UpdatedBy { get; private set; } = updatedBy;
    public Guid? DeletedBy { get; private set; } = deletedBy;
    public VariantEntity[] Variants { get; private set; } = [];

    public static ProductAggregateRoot CreateOne(
        ICreateOneProductCommand command,
        ICreateOneProductServiceResponse data
    )
    {
        var variantOnAttributeEntities = VariantOnAttributeEntityFactory.CreateMany(
            variantsOnAttributes: command.VariantOnAttributeDtos,
            variantsOnAttributesFoundById: data.VariantsOnAttributesFoundById,
            attributesFoundById: data.AttributesFoundById
        );

        var variantEntities = VariantEntityFactory.CreateMany(
            variants: command.VariantDtos,
            variantsFoundById: data.VariantsFoundById
        );

        var variantOnAttributeEntitiesGroupByVariantId = variantOnAttributeEntities
            .AsParallel()
            .GroupBy(variantOnAttribute => variantOnAttribute.VariantId)
            .ToDictionary(g => g.Key, g => g.ToArray());

        if (variantOnAttributeEntitiesGroupByVariantId.Count != variantEntities.Length)
        {
            throw new ConflictException("VariantOnAttribute not belong to this variant");
        }

        foreach (var variant in variantEntities)
        {
            variant.AttachManyVariantOnAttribute(
                variantOnAttributeEntitiesGroupByVariantId[variant.Id]
            );
        }

        var productAggregateRoot = ProductAggregateRootFactory.CreateOne(
            product: command.ProductDto,
            data: data
        );

        productAggregateRoot.AttachManyVariants(variantEntities);

        return productAggregateRoot;
    }

    public static ProductAggregateRoot UpdateOne(
        IUpdateOneProductCommand command,
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

        if (data.ProductFoundById.Id != command.ProductDto.Id)
        {
            throw new ConflictException("Product id does not match");
        }

        return new ProductAggregateRoot(
            id: data.ProductFoundById.Id,
            name: command.ProductDto.Name ?? data.ProductFoundById.Name,
            description: command.ProductDto.Description ?? data.ProductFoundById.Description,
            brand: command.ProductDto.Brand ?? data.ProductFoundById.Brand,
            model: command.ProductDto.Model ?? data.ProductFoundById.Model,
            isCompleted: data.ProductFoundById.IsCompleted,
            isPublished: data.ProductFoundById.IsPublished,
            isDeleted: data.ProductFoundById.IsDeleted,
            createdAtDatetime: data.ProductFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: data.ProductFoundById.DeletedAtDatetime,
            createdAtTimezone: data.ProductFoundById.CreatedAtTimezone,
            updatedAtTimezone: command.ProductDto.UpdatedAtTimezone,
            deletedAtTimezone: data.ProductFoundById.DeletedAtTimezone,
            createdBy: data.ProductFoundById.CreatedBy,
            updatedBy: command.ProductDto.UpdatedBy,
            deletedBy: data.ProductFoundById.DeletedBy
        );
    }

    private void AttachManyVariants(VariantEntity[] variants)
    {
        foreach (var variant in variants)
            AttachOneVariant(variant);
    }

    private void AttachOneVariant(VariantEntity variant)
    {
        if (IsVariantExist(variant))
        {
            throw new ConflictException("Variant exist with this id");
        }

        if (!VariantBelongToProduct(variant))
        {
            throw new ConflictException("Variant not belong to this product");
        }

        if (variant.IsDeleted)
        {
            throw new ConflictException("Variant deleted cannot be attached");
        }

        Variants = [.. Variants, variant];
    }

    private bool IsVariantExist(VariantEntity variant)
    {
        return Variants.Any(variant.IsEqual);
    }

    private bool VariantBelongToProduct(VariantEntity variant)
    {
        return variant.ProductId == Id;
    }
}

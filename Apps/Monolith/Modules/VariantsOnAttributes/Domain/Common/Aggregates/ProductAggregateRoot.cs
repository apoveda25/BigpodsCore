using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Commands;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Services;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Entities;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Factories;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Commands;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Services;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Aggregates;

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

    private void AttachManyVariants(VariantEntity[] variants)
    {
        foreach (var variant in variants)
        {
            AttachOneVariant(variant);
        }
    }

    private void AttachOneVariant(VariantEntity variantEntity)
    {
        if (IsVariantNotBelongToProduct(variantEntity))
        {
            throw new ConflictException("Variant not belong to this product");
        }

        if (IsVariantAlreadyAttached(variantEntity))
        {
            throw new ConflictException("Variant already attached");
        }

        if (variantEntity.IsDeleted)
        {
            throw new ConflictException("Variant is deleted");
        }

        Variants = [.. Variants, variantEntity];
    }

    public static ProductAggregateRoot AttachManyVariantsOnAttributes(
        IAttachManyVariantOnAttributeCommand command,
        IAttachManyVariantOnAttributeServiceResponse data
    )
    {
        var variantOnAttributeEntities = VariantOnAttributeEntityFactory.CreateMany(
            variantsOnAttributes: command.VariantOnAttributeDtos,
            variantsOnAttributesFoundById: data.VariantsOnAttributesFoundById,
            variantsOnAttributesFoundByVariantIdAttributeId: data.VariantsOnAttributesFoundByVariantIdAttributeId,
            attributesFoundById: data.AttributesFoundById
        );

        var variantEntities = VariantEntityFactory.BuildMany(data.VariantsFoundById);

        var variantOnAttributeEntitiesDict = variantOnAttributeEntities
            .AsParallel()
            .GroupBy(variantOnAttributeEntity => variantOnAttributeEntity.VariantId)
            .ToDictionary(group => group.Key, group => group.ToArray());

        if (variantEntities.Length != variantOnAttributeEntitiesDict.Keys.Count)
        {
            throw new ConflictException("Variants not found");
        }

        foreach (var variantEntity in variantEntities)
        {
            variantEntity.AttachManyVariantsOnAttributes(
                variantOnAttributeEntities: variantOnAttributeEntitiesDict.GetValueOrDefault(
                    variantEntity.Id,
                    []
                )
            );
        }

        var productAggregateRoot = ProductAggregateRootFactory.BuildOne(data.ProductFoundById);

        productAggregateRoot.AttachManyVariants(variantEntities);

        return productAggregateRoot;
    }

    public static ProductAggregateRoot DettachManyVariantsOnAttributes(
        IDettachManyVariantOnAttributeCommand command,
        IDettachManyVariantOnAttributeServiceResponse data
    )
    {
        var variantOnAttributeEntities = VariantOnAttributeEntityFactory.DeleteMany(
            variantsOnAttributes: command.VariantOnAttributeDtos,
            variantsOnAttributesFoundById: data.VariantsOnAttributesFoundById
        );

        var variantEntities = VariantEntityFactory.BuildMany(data.VariantsFoundById);

        var variantOnAttributesEntitiesDict = variantOnAttributeEntities
            .AsParallel()
            .GroupBy(variantOnAttributeEntity => variantOnAttributeEntity.VariantId)
            .ToDictionary(group => group.Key, group => group.ToArray());

        if (variantEntities.Length != variantOnAttributesEntitiesDict.Keys.Count)
        {
            throw new ConflictException("Variants not found");
        }

        foreach (var variantEntity in variantEntities)
        {
            variantEntity.DettachManyVariantsOnAttributes(
                variantOnAttributeEntities: variantOnAttributesEntitiesDict.GetValueOrDefault(
                    variantEntity.Id,
                    []
                )
            );
        }

        var productAggregateRoot = ProductAggregateRootFactory.BuildOne(data.ProductFoundById);

        productAggregateRoot.AttachManyVariants(variantEntities);

        return productAggregateRoot;
    }

    private bool IsVariantNotBelongToProduct(VariantEntity variant)
    {
        return variant.ProductId != Id;
    }

    private bool IsVariantAlreadyAttached(VariantEntity variantEntity)
    {
        return Variants.Any(variantEntity.IsIsEqual);
    }
}

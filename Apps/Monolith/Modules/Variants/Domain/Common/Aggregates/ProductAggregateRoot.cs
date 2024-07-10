using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Factories;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Services;

namespace Bigpods.Monolith.Modules.Variants.Domain.Common.Aggregates;

public sealed partial class ProductAggregateRoot(
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

    public static ProductAggregateRoot CreateOneVariant(
        ICreateOneVariantCommand command,
        ICreateOneVariantServiceResponse data
    )
    {
        var variantOnAttributeEntities = VariantOnAttributeEntityFactory.CreateMany(
            variantsOnAttributes: command.VariantOnAttributeDtos,
            variantsOnAttributesFoundById: data.VariantsOnAttributesFoundById,
            variantsOnAttributesFoundByVariantIdAttributeId: data.VariantsOnAttributesFoundByVariantIdAttributeId,
            attributesFoundById: data.AttributesFoundById
        );

        var variantEntity = VariantEntityFactory.CreateOne(
            variant: command.VariantDto,
            variantFoundById: data.VariantFoundById
        );

        variantEntity.AttachManyVariantOnAttribute(variantOnAttributeEntities);

        var aggregateRoot = ProductAggregateRootFactory.BuildOne(
            productFoundById: data.ProductFoundById
        );

        aggregateRoot.AttachOneVariant(variantEntity);

        return aggregateRoot;
    }

    public static ProductAggregateRoot UpdateOneVariant(
        IUpdateOneVariantCommand command,
        IUpdateOneVariantServiceResponse data
    )
    {
        var variantEntity = VariantEntityFactory.UpdateOne(
            variant: command.VariantDto,
            variantFoundById: data.VariantFoundById
        );

        var aggregateRoot = ProductAggregateRootFactory.BuildOne(
            productFoundById: data.ProductFoundById
        );

        aggregateRoot.AttachOneVariant(variantEntity);

        return aggregateRoot;
    }

    public static ProductAggregateRoot DeleteOneVariant(
        IDeleteOneVariantCommand command,
        IDeleteOneVariantServiceResponse data
    )
    {
        var variantOnAttributeEntities = VariantOnAttributeEntityFactory.DeleteMany(
            variant: command.VariantDto,
            variantsOnAttributesFoundByVariantId: data.VariantsOnAttributesFoundByVariantId
        );

        var variantEntity = VariantEntityFactory.DeleteOne(
            variant: command.VariantDto,
            variantFoundById: data.VariantFoundById,
            inventoryFoundByVariantId: data.InventoryFoundByVariantId
        );

        variantEntity.DettachManyVariantOnAttribute(variantOnAttributeEntities);

        var aggregateRoot = ProductAggregateRootFactory.BuildOne(
            productFoundById: data.ProductFoundById
        );

        aggregateRoot.DettachOneVariant(variantEntity);

        return aggregateRoot;
    }

    private void AttachOneVariant(VariantEntity variant)
    {
        if (IsVariantExist(variant))
        {
            throw new ConflictException("Variant exist with this id");
        }

        if (!IsVariantBelongToProduct(variant))
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
        if (IsVariantExist(variant))
        {
            throw new ConflictException("Variant not exist with this id");
        }

        if (!IsVariantBelongToProduct(variant))
        {
            throw new ConflictException("Variant not belong to this product");
        }

        if (!variant.IsDeleted)
        {
            throw new ConflictException("Do not dettach variant marked as not deleted");
        }

        Variants = [.. Variants, variant];
    }

    private bool IsVariantExist(VariantEntity variantEntity)
    {
        return Variants.Any(variantEntity.IsIsEqual);
    }

    private bool IsVariantBelongToProduct(VariantEntity variantEntity)
    {
        return variantEntity.ProductId == Id;
    }
}

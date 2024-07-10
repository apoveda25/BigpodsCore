using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;

public sealed partial class VariantEntity(
    Guid? id = null,
    string? name = null,
    string? sku = null,
    decimal? price = null,
    decimal? cost = null,
    bool? isDeleted = null,
    DateTime? createdAtDatetime = null,
    DateTime? updatedAtDatetime = null,
    DateTime? deletedAtDatetime = null,
    string? createdAtTimezone = null,
    string? updatedAtTimezone = null,
    string? deletedAtTimezone = null,
    Guid? createdBy = null,
    Guid? updatedBy = null,
    Guid? deletedBy = null,
    Guid? productId = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public NameVO Name { get; private set; } = new NameVO(name ?? string.Empty);
    public string Sku { get; private set; } = sku ?? string.Empty;
    public PriceVO Price { get; private set; } = new PriceVO(price ?? 0);
    public CostVO Cost { get; private set; } = new CostVO(cost ?? 0);
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
    public Guid ProductId { get; private set; } = productId ?? Guid.Empty;
    public VariantOnAttributeEntity[] VariantsOnAttributes { get; private set; } = [];

    public void AttachManyVariantOnAttribute(VariantOnAttributeEntity[] variantsOnAttributes)
    {
        foreach (var variantOnAttribute in variantsOnAttributes)
            AttachOneVariantOnAttribute(variantOnAttribute);
    }

    public void AttachOneVariantOnAttribute(VariantOnAttributeEntity variantOnAttribute)
    {
        if (IsVariantOnAttributeExist(variantOnAttribute))
        {
            throw new ConflictException("VariantOnAttribute exist with this id or attributeId");
        }

        if (!VariantOnAttributeBelongToVariant(variantOnAttribute))
        {
            throw new ConflictException("VariantOnAttribute not belong to this variant");
        }

        if (variantOnAttribute.IsDeleted)
        {
            throw new ConflictException("Do not attach variantOnAttribute marked as deleted");
        }

        VariantsOnAttributes = [.. VariantsOnAttributes, variantOnAttribute];
    }

    public void DettachManyVariantOnAttribute(VariantOnAttributeEntity[] variantsOnAttributes)
    {
        foreach (var variantOnAttribute in variantsOnAttributes)
            DettachOneVariantOnAttribute(variantOnAttribute);
    }

    public void DettachOneVariantOnAttribute(VariantOnAttributeEntity variantOnAttribute)
    {
        if (IsVariantOnAttributeExist(variantOnAttribute))
        {
            throw new ConflictException("VariantOnAttribute not exist with this id or attributeId");
        }

        if (!VariantOnAttributeBelongToVariant(variantOnAttribute))
        {
            throw new ConflictException("VariantOnAttribute not belong to this variant");
        }

        if (!variantOnAttribute.IsDeleted)
        {
            throw new ConflictException("Do not dettach variantOnAttribute marked as not deleted");
        }

        VariantsOnAttributes = [.. VariantsOnAttributes, variantOnAttribute];
    }

    public bool IsIsEqual(VariantEntity variant)
    {
        return Id == variant.Id;
    }

    private bool IsVariantOnAttributeExist(VariantOnAttributeEntity entity)
    {
        return VariantsOnAttributes.Any(entity.IsIsEqual);
    }

    private bool VariantOnAttributeBelongToVariant(
        VariantOnAttributeEntity variantOnAttributeEntity
    )
    {
        return variantOnAttributeEntity.VariantId == Id;
    }
}

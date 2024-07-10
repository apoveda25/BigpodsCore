namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Entities;

public sealed class VariantOnAttributeEntity(
    Guid? id = null,
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
    Guid? variantId = null,
    Guid? attributeId = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
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
    public Guid VariantId { get; private set; } = variantId ?? Guid.Empty;
    public Guid AttributeId { get; private set; } = attributeId ?? Guid.Empty;

    public bool IsIsEqual(VariantOnAttributeEntity variantOnAttribute)
    {
        return Id == variantOnAttribute.Id
            || (
                VariantId == variantOnAttribute.VariantId
                && AttributeId == variantOnAttribute.AttributeId
            );
    }
}

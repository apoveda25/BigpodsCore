using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Attributes.Domain.Common.Entities;

public sealed class AttributeEntity(
    Guid? id = null,
    string? value = null,
    string? valuePattern = null,
    string? measuringUnit = null,
    string? measuringUnitPattern = null,
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
    Guid? attributeTypeId = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public PatternVO Value { get; private set; } =
        new PatternVO(value ?? string.Empty, valuePattern ?? string.Empty);
    public PatternVO MeasuringUnit { get; private set; } =
        new PatternVO(measuringUnit ?? string.Empty, measuringUnitPattern ?? string.Empty);
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
    public Guid AttributeTypeId { get; private set; } = attributeTypeId ?? Guid.Empty;

    public bool IsEqual(AttributeEntity attribute)
    {
        return Id == attribute.Id
            || (
                Value.Value == attribute.Value.Value
                && MeasuringUnit.Value == attribute.MeasuringUnit.Value
                && AttributeTypeId == attribute.AttributeTypeId
            );
    }
}

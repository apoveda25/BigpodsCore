using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Attributes.Domain.Common.Entities;

public sealed class AttributeTypeEntity
{
    public Guid Id { get; private set; }
    public WordVO Name { get; private set; }
    public SentenceVO Description { get; private set; }
    public string ValuePattern { get; private set; }
    public string MeasuringUnitPattern { get; private set; }
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

    private AttributeTypeEntity(
        Guid id,
        string name,
        string description,
        string valuePattern,
        string measuringUnitPattern,
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
        Name = new WordVO(name);
        Description = new SentenceVO(description);
        ValuePattern = valuePattern;
        MeasuringUnitPattern = measuringUnitPattern;
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
    }

    public static AttributeTypeEntity BuildOne(
        IAttributeTypeModel? attributeType
    )
    {
        if (attributeType is null)
        {
            throw new NotFoundException("Attribute type does not exist with this id");
        }

        if (attributeType.IsDeleted)
        {
            throw new ConflictException("Attribute type is deleted");
        }

        return new AttributeTypeEntity(
            id: attributeType.Id,
            name: attributeType.Name,
            description: attributeType.Description,
            valuePattern: attributeType.ValuePattern,
            measuringUnitPattern: attributeType.MeasuringUnitPattern,
            isDeleted: attributeType.IsDeleted,
            createdAtDatetime: attributeType.CreatedAtDatetime,
            updatedAtDatetime: attributeType.UpdatedAtDatetime,
            deletedAtDatetime: attributeType.DeletedAtDatetime,
            createdAtTimezone: attributeType.CreatedAtTimezone,
            updatedAtTimezone: attributeType.UpdatedAtTimezone,
            deletedAtTimezone: attributeType.DeletedAtTimezone,
            createdBy: attributeType.CreatedBy,
            updatedBy: attributeType.UpdatedBy,
            deletedBy: attributeType.DeletedBy
        );
    }
}

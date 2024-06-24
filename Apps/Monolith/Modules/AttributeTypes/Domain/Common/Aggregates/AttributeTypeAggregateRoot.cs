using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.Common.Aggregates;

public sealed class AttributeTypeAggregateRoot
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

    private AttributeTypeAggregateRoot(
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

    public static AttributeTypeAggregateRoot CreateOne(
        ICreateOneAttributeTypeDto attributeType,
        IAttributeTypeModel? attributeTypeFoundById,
        IAttributeTypeModel? attributeTypeFoundByName
    )
    {
        if (attributeTypeFoundById is not null)
        {
            throw new ConflictException("Attribute type exist with this id");
        }

        if (attributeTypeFoundByName is not null)
        {
            throw new ConflictException("Attribute type exist with this name");
        }

        return new AttributeTypeAggregateRoot(
            id: attributeType.Id,
            name: attributeType.Name,
            description: attributeType.Description,
            valuePattern: attributeType.ValuePattern,
            measuringUnitPattern: attributeType.MeasuringUnitPattern,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: attributeType.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: attributeType.CreatedBy,
            updatedBy: null,
            deletedBy: null
        );
    }

    public static AttributeTypeAggregateRoot UpdateOne(
        IUpdateOneAttributeTypeDto attributeType,
        IAttributeTypeModel? attributeTypeFoundById,
        IAttributeTypeModel? attributeTypeFoundByName
    )
    {
        if (attributeTypeFoundById is null)
        {
            throw new NotFoundException("Attribute type not exist with this id");
        }

        if (attributeTypeFoundByName is not null && attributeTypeFoundByName.Id != attributeType.Id)
        {
            throw new ConflictException("Attribute type exist with this name");
        }

        if (attributeTypeFoundById.IsDeleted)
        {
            throw new ConflictException("Attribute type is deleted");
        }

        if (attributeTypeFoundById.Id != attributeType.Id)
        {
            throw new ConflictException("Attribute type id does not match");
        }

        return new AttributeTypeAggregateRoot(
            id: attributeTypeFoundById.Id,
            name: attributeType.Name ?? attributeTypeFoundById.Name,
            description: attributeType.Description ?? attributeTypeFoundById.Description,
            valuePattern: attributeType.ValuePattern ?? attributeTypeFoundById.ValuePattern,
            measuringUnitPattern: attributeType.MeasuringUnitPattern ?? attributeTypeFoundById.MeasuringUnitPattern,
            isDeleted: attributeTypeFoundById.IsDeleted,
            createdAtDatetime: attributeTypeFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: attributeTypeFoundById.DeletedAtDatetime,
            createdAtTimezone: attributeTypeFoundById.CreatedAtTimezone,
            updatedAtTimezone: attributeType.UpdatedAtTimezone,
            deletedAtTimezone: attributeTypeFoundById.DeletedAtTimezone,
            createdBy: attributeTypeFoundById.CreatedBy,
            updatedBy: attributeType.UpdatedBy,
            deletedBy: attributeTypeFoundById.DeletedBy
        );
    }
}

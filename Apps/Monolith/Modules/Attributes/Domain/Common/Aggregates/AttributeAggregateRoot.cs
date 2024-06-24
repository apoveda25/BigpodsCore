using Bigpods.Monolith.Modules.Attributes.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Attributes.Domain.Common.Aggregates;

public sealed class AttributeAggregateRoot
{
    public Guid Id { get; private set; }
    public PatternVO Value { get; private set; }
    public PatternVO MeasuringUnit { get; private set; }
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
    public Guid AttributeTypeId { get; private set; }
    public AttributeTypeEntity AttributeType { get; private set; }

    private AttributeAggregateRoot(
        Guid id,
        string value,
        string measuringUnit,
        bool isDeleted,
        DateTime createdAtDatetime,
        DateTime? updatedAtDatetime,
        DateTime? deletedAtDatetime,
        string createdAtTimezone,
        string? updatedAtTimezone,
        string? deletedAtTimezone,
        Guid createdBy,
        Guid? updatedBy,
        Guid? deletedBy,
        AttributeTypeEntity attributeType
    )
    {
        Id = id;
        Value = new PatternVO(value, attributeType.ValuePattern);
        MeasuringUnit = new PatternVO(measuringUnit, attributeType.MeasuringUnitPattern);
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
        AttributeTypeId = attributeType.Id;
        AttributeType = attributeType;
    }

    public static AttributeAggregateRoot CreateOne(
        ICreateOneAttributeDto attribute,
        AttributeTypeEntity attributeType,
        IAttributeModel? attributeFoundById,
        IAttributeModel? attributeFoundByValueMeasuringUnitAttributeTypeId
    )
    {
        if (attributeFoundById is not null)
        {
            throw new ConflictException("Attribute already exists with this id");
        }

        if (attributeFoundByValueMeasuringUnitAttributeTypeId is not null && attributeFoundByValueMeasuringUnitAttributeTypeId?.IsDeleted == false)
        {
            throw new ConflictException("Attribute already exists with this type");
        }

        return new AttributeAggregateRoot(
            id: attribute.Id,
            value: attribute.Value,
            measuringUnit: attribute.MeasuringUnit,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: attribute.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: attribute.CreatedBy,
            updatedBy: null,
            deletedBy: null,
            attributeType: attributeType
        );
    }

    public static AttributeAggregateRoot DeleteOne(
        IDeleteOneAttributeDto attribute,
        AttributeTypeEntity attributeType,
        IAttributeModel? attributeFoundById
    )
    {
        if (attributeFoundById is null)
        {
            throw new NotFoundException("Attribute not exist with this id");
        }

        if (attributeFoundById.IsDeleted == true)
        {
            throw new ConflictException("Attribute is deleted");
        }

        if (attributeFoundById.Id != attribute.Id)
        {
            throw new ConflictException("Attribute id not match");
        }

        return new AttributeAggregateRoot(
            id: attributeFoundById.Id,
            value: attributeFoundById.Value,
            measuringUnit: attributeFoundById.MeasuringUnit,
            isDeleted: true,
            createdAtDatetime: attributeFoundById.CreatedAtDatetime,
            updatedAtDatetime: attributeFoundById.UpdatedAtDatetime,
            deletedAtDatetime: DateTime.Now,
            createdAtTimezone: attributeFoundById.CreatedAtTimezone,
            updatedAtTimezone: attributeFoundById.UpdatedAtTimezone,
            deletedAtTimezone: attribute.DeletedAtTimezone,
            createdBy: attributeFoundById.CreatedBy,
            updatedBy: attributeFoundById.UpdatedBy,
            deletedBy: attribute.DeletedBy,
            attributeType: attributeType
        );
    }
}

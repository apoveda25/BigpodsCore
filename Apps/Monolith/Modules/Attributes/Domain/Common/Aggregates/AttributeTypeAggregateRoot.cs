using Bigpods.Monolith.Modules.Attributes.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Attributes.Domain.Common.Factories;
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Attributes.Domain.Common.Aggregates;

public sealed class AttributeTypeAggregateRoot(
    Guid? id = null,
    string? name = null,
    string? description = null,
    string? valuePattern = null,
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
    Guid? deletedBy = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public WordVO Name { get; private set; } = new WordVO(name ?? string.Empty);
    public SentenceVO Description { get; private set; } =
        new SentenceVO(description ?? string.Empty);
    public string ValuePattern { get; private set; } = valuePattern ?? string.Empty;
    public string MeasuringUnitPattern { get; private set; } = measuringUnitPattern ?? string.Empty;
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
    public AttributeEntity[] Attributes { get; private set; } = [];

    public static AttributeTypeAggregateRoot CreateOneAttribute(
        ICreateOneAttributeCommand command,
        ICreateOneAttributeServiceResponse data
    )
    {
        var aggregateRoot = AttributeTypeAggregateRootFactory.BuildOne(data.AttributeTypeFoundById);

        var attributeEntity = AttributeEntityFactory.CreateOne(
            attribute: command.AttributeDto,
            attributeFoundById: data.AttributeFoundById,
            attributeFoundByValueMeasuringUnitAttributeTypeId: data.AttributeFoundByValueMeasuringUnitAttributeTypeId,
            attributeTypeFoundById: data.AttributeTypeFoundById
        );

        aggregateRoot.AttachOneAttribute(attributeEntity);

        return aggregateRoot;
    }

    public static AttributeTypeAggregateRoot DeleteOneAttribute(
        IDeleteOneAttributeCommand command,
        IDeleteOneAttributeServiceResponse data
    )
    {
        var aggregateRoot = AttributeTypeAggregateRootFactory.BuildOne(data.AttributeTypeFoundById);

        var attributeEntity = AttributeEntityFactory.DeleteOne(
            attribute: command.AttributeDto,
            attributeFoundById: data.AttributeFoundById,
            attributeTypeFoundById: data.AttributeTypeFoundById
        );

        aggregateRoot.DettachOneAttribute(attributeEntity);

        return aggregateRoot;
    }

    private void AttachOneAttribute(AttributeEntity attribute)
    {
        if (attribute.IsDeleted)
        {
            throw new ConflictException("Attribute deleted cannot be attached");
        }

        if (IsAttributeAttached(attribute))
        {
            throw new ConflictException(
                "Attribute attached with this id or value, measuring unit and attribute type"
            );
        }

        if (IsNotAttributeBelongToAttributeType(attribute))
        {
            throw new ConflictException("Attribute not belong to this attribute type");
        }

        Attributes = [.. Attributes, attribute];
    }

    private void DettachOneAttribute(AttributeEntity attribute)
    {
        if (!attribute.IsDeleted)
        {
            throw new ConflictException("Attribute not deleted can not be dettached");
        }

        if (IsAttributeAttached(attribute))
        {
            throw new ConflictException(
                "Attribute not attached with this id or value, measuring unit and attribute type"
            );
        }

        if (IsNotAttributeBelongToAttributeType(attribute))
        {
            throw new ConflictException("Attribute not belong to this attribute type");
        }

        Attributes = [.. Attributes, attribute];
    }

    private bool IsAttributeAttached(AttributeEntity attribute)
    {
        return Attributes.Any(attribute.IsEqual);
    }

    private bool IsNotAttributeBelongToAttributeType(AttributeEntity attribute)
    {
        return attribute.AttributeTypeId != Id;
    }
}

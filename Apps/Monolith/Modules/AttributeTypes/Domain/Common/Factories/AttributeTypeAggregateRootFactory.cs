using Bigpods.Monolith.Modules.AttributeTypes.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.Common.Factories;

public sealed class AttributeTypeAggregateRootFactory
{
    public static AttributeTypeAggregateRoot CreateOne(
        ICreateOneAttributeTypeDto attributeType,
        ICreateOneAttributeTypeServiceResponse data
    )
    {
        if (data.AttributeTypeFoundById is not null)
        {
            throw new ConflictException("Attribute type exist with this id");
        }

        if (data.AttributeTypeFoundByName is not null)
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
            createdAtTimezone: attributeType.CreatedAtTimezone,
            createdBy: attributeType.CreatedBy
        );
    }

    public static AttributeTypeAggregateRoot UpdateOne(
        IUpdateOneAttributeTypeDto attributeType,
        IUpdateOneAttributeTypeServiceResponse data
    )
    {
        if (data.AttributeTypeFoundById is null)
        {
            throw new NotFoundException("Attribute type not exist with this id");
        }

        if (
            data.AttributeTypeFoundByName is not null
            && data.AttributeTypeFoundByName.Id != attributeType.Id
        )
        {
            throw new ConflictException("Attribute type exist with this name");
        }

        if (data.AttributeTypeFoundById.IsDeleted)
        {
            throw new ConflictException("Attribute type is deleted");
        }

        if (data.AttributeTypeFoundById.Id != attributeType.Id)
        {
            throw new ConflictException("Attribute type id does not match");
        }

        return new AttributeTypeAggregateRoot(
            id: data.AttributeTypeFoundById.Id,
            name: attributeType.Name ?? data.AttributeTypeFoundById.Name,
            description: attributeType.Description ?? data.AttributeTypeFoundById.Description,
            valuePattern: attributeType.ValuePattern ?? data.AttributeTypeFoundById.ValuePattern,
            measuringUnitPattern: attributeType.MeasuringUnitPattern
                ?? data.AttributeTypeFoundById.MeasuringUnitPattern,
            isDeleted: data.AttributeTypeFoundById.IsDeleted,
            createdAtDatetime: data.AttributeTypeFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: data.AttributeTypeFoundById.DeletedAtDatetime,
            createdAtTimezone: data.AttributeTypeFoundById.CreatedAtTimezone,
            updatedAtTimezone: attributeType.UpdatedAtTimezone,
            deletedAtTimezone: data.AttributeTypeFoundById.DeletedAtTimezone,
            createdBy: data.AttributeTypeFoundById.CreatedBy,
            updatedBy: attributeType.UpdatedBy,
            deletedBy: data.AttributeTypeFoundById.DeletedBy
        );
    }
}

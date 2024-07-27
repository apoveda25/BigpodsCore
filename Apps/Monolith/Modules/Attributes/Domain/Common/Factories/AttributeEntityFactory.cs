using Bigpods.Monolith.Modules.Attributes.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Attributes.Domain.Common.Factories;

public static class AttributeEntityFactory
{
    public static AttributeEntity CreateOne(
        ICreateOneAttributeDto attribute,
        IAttributeModel? attributeFoundById,
        IAttributeModel? attributeFoundByValueMeasuringUnitAttributeTypeId,
        IAttributeTypeModel? attributeTypeFoundById
    )
    {
        if (attributeFoundById is not null)
        {
            throw new ConflictException("Attribute already exists with this id");
        }

        if (attributeFoundByValueMeasuringUnitAttributeTypeId is not null)
        {
            throw new ConflictException(
                "Attribute already exists with this value, measuring unit and attribute type"
            );
        }

        if (attributeTypeFoundById is null)
        {
            throw new NotFoundException("Attribute type not exist with this id");
        }

        if (attributeTypeFoundById.Id != attribute.AttributeTypeId)
        {
            throw new ConflictException("Attribute type not belong to this attribute");
        }

        return new AttributeEntity(
            id: attribute.Id,
            value: attribute.Value,
            valuePattern: attributeTypeFoundById.ValuePattern,
            measuringUnit: attribute.MeasuringUnit,
            measuringUnitPattern: attributeTypeFoundById.MeasuringUnitPattern,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            createdAtTimezone: attribute.CreatedAtTimezone,
            createdBy: attribute.CreatedBy,
            attributeTypeId: attributeTypeFoundById.Id
        );
    }

    public static AttributeEntity DeleteOne(
        IDeleteOneAttributeDto attribute,
        IAttributeModel? attributeFoundById,
        IAttributeTypeModel? attributeTypeFoundById
    )
    {
        if (attributeTypeFoundById is null)
        {
            throw new NotFoundException("Attribute type not exist with this id");
        }

        if (attributeFoundById is null)
        {
            throw new NotFoundException("Attribute not exist with this id");
        }

        if (attributeFoundById.IsDeleted)
        {
            throw new ConflictException("Attribute is deleted");
        }

        if (attributeFoundById.Id != attribute.Id)
        {
            throw new ConflictException("Attribute id not match");
        }

        return new AttributeEntity(
            id: attributeFoundById.Id,
            value: attributeFoundById.Value,
            valuePattern: attributeTypeFoundById.ValuePattern,
            measuringUnit: attributeFoundById.MeasuringUnit,
            measuringUnitPattern: attributeTypeFoundById.MeasuringUnitPattern,
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
            attributeTypeId: attributeFoundById.AttributeTypeId
        );
    }
}

using Bigpods.Monolith.Modules.Attributes.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Attributes.Domain.Common.Factories;

public sealed class AttributeTypeAggregateRootFactory
{
    public static AttributeTypeAggregateRoot BuildOne(IAttributeTypeModel? attributeType)
    {
        if (attributeType is null)
        {
            throw new NotFoundException("Attribute type not exist with this id");
        }

        if (attributeType.IsDeleted)
        {
            throw new ConflictException("Attribute type is deleted");
        }

        return new AttributeTypeAggregateRoot(
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

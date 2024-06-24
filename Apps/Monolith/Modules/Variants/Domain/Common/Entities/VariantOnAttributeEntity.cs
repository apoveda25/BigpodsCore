using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;

public sealed class VariantOnAttributeEntity
{
    public Guid Id { get; private set; }
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
    public Guid VariantId { get; private set; }
    public Guid AttributeId { get; private set; }

    private VariantOnAttributeEntity(
        Guid id,
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
        Guid variantId,
        Guid attributeId
    )
    {
        Id = id;
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
        VariantId = variantId;
        AttributeId = attributeId;
    }

    public static VariantOnAttributeEntity Build(
        IVariantOnAttributeModel variantOnAttribute
    )
    {
        return new VariantOnAttributeEntity
        (
            id: variantOnAttribute.Id,
            isDeleted: variantOnAttribute.IsDeleted,
            createdAtDatetime: variantOnAttribute.CreatedAtDatetime,
            updatedAtDatetime: variantOnAttribute.UpdatedAtDatetime,
            deletedAtDatetime: variantOnAttribute.DeletedAtDatetime,
            createdAtTimezone: variantOnAttribute.CreatedAtTimezone,
            updatedAtTimezone: variantOnAttribute.UpdatedAtTimezone,
            deletedAtTimezone: variantOnAttribute.DeletedAtTimezone,
            createdBy: variantOnAttribute.CreatedBy,
            updatedBy: variantOnAttribute.UpdatedBy,
            deletedBy: variantOnAttribute.DeletedBy,
            variantId: variantOnAttribute.VariantId,
            attributeId: variantOnAttribute.AttributeId
        );
    }

    public static VariantOnAttributeEntity CreateOne(
        ICreateOneVariantOnAttributeDto variantOnAttribute,
        IVariantOnAttributeModel[] variantsOnAttributesFoundById,
        IAttributeModel[] attributesFoundById
    )
    {
        if (variantsOnAttributesFoundById.Length != 0)
        {
            throw new ConflictException("VariantsOnAttributes exist with this id");
        }

        if (
            attributesFoundById.FirstOrDefault(
                attr => attr.Id == variantOnAttribute.AttributeId && attr.IsDeleted == false
            ) is null
        )
        {
            throw new NotFoundException("Attributes not found");
        }

        return new VariantOnAttributeEntity(
            id: variantOnAttribute.Id,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: variantOnAttribute.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: variantOnAttribute.CreatedBy,
            updatedBy: null,
            deletedBy: null,
            variantId: variantOnAttribute.VariantId,
            attributeId: variantOnAttribute.AttributeId
        );
    }

    public bool IsEquals(VariantOnAttributeEntity variantOnAttribute)
    {
        return Id == variantOnAttribute.Id || (VariantId == variantOnAttribute.VariantId && AttributeId == variantOnAttribute.AttributeId);
    }

    public static VariantOnAttributeEntity DeleteOne(
        IDeleteOneVariantDto variant,
        IVariantOnAttributeModel? variantOnAttributeFoundByVariantId
    )
    {
        if (variantOnAttributeFoundByVariantId is null)
        {
            throw new NotFoundException("VariantOnAttribute not found with this variantId");
        }

        if (variantOnAttributeFoundByVariantId.VariantId != variant.Id)
        {
            throw new ConflictException("VariantOnAttribute not match with this variantId");
        }

        if (variantOnAttributeFoundByVariantId.IsDeleted)
        {
            throw new ConflictException("VariantOnAttribute already deleted");
        }

        return new VariantOnAttributeEntity
        (
            id: variantOnAttributeFoundByVariantId.Id,
            isDeleted: true,
            createdAtDatetime: variantOnAttributeFoundByVariantId.CreatedAtDatetime,
            updatedAtDatetime: variantOnAttributeFoundByVariantId.UpdatedAtDatetime,
            deletedAtDatetime: DateTime.Now,
            createdAtTimezone: variantOnAttributeFoundByVariantId.CreatedAtTimezone,
            updatedAtTimezone: variantOnAttributeFoundByVariantId.UpdatedAtTimezone,
            deletedAtTimezone: variant.DeletedAtTimezone,
            createdBy: variantOnAttributeFoundByVariantId.CreatedBy,
            updatedBy: variantOnAttributeFoundByVariantId.UpdatedBy,
            deletedBy: variant.DeletedBy,
            variantId: variantOnAttributeFoundByVariantId.VariantId,
            attributeId: variantOnAttributeFoundByVariantId.AttributeId
        );
    }
}

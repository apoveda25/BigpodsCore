using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Domain.Common.Factories;

public static class VariantOnAttributeEntityFactory
{
    public static VariantOnAttributeEntity[] CreateMany(
        ICreateOneVariantOnAttributeDto[] variantsOnAttributes,
        IVariantOnAttributeModel[] variantsOnAttributesFoundById,
        IVariantOnAttributeModel[] variantsOnAttributesFoundByVariantIdAttributeId,
        IAttributeModel[] attributesFoundById
    )
    {
        var variantsOnAttributesFoundByIdDict = variantsOnAttributesFoundById
            .AsParallel()
            .ToDictionary(x => x.Id);
        var variantsOnAttributesFoundByVariantIdAttributeIdDict =
            variantsOnAttributesFoundByVariantIdAttributeId
                .AsParallel()
                .ToDictionary(x => $"{x.VariantId}:{x.AttributeId}");
        var attributesFoundByIdDict = attributesFoundById.AsParallel().ToDictionary(x => x.Id);

        return variantsOnAttributes
            .Select(variantOnAttribute =>
                CreateOne(
                    variantOnAttribute: variantOnAttribute,
                    variantOnAttributeFoundById: variantsOnAttributesFoundByIdDict.GetValueOrDefault(
                        variantOnAttribute.Id
                    ),
                    variantOnAttributeFoundByVariantIdAttributeId: variantsOnAttributesFoundByVariantIdAttributeIdDict.GetValueOrDefault(
                        $"{variantOnAttribute.VariantId}:{variantOnAttribute.AttributeId}"
                    ),
                    attributeFoundById: attributesFoundByIdDict.GetValueOrDefault(
                        variantOnAttribute.AttributeId
                    )
                )
            )
            .ToArray();
    }

    public static VariantOnAttributeEntity CreateOne(
        ICreateOneVariantOnAttributeDto variantOnAttribute,
        IVariantOnAttributeModel? variantOnAttributeFoundById,
        IVariantOnAttributeModel? variantOnAttributeFoundByVariantIdAttributeId,
        IAttributeModel? attributeFoundById
    )
    {
        if (variantOnAttributeFoundById is not null)
        {
            throw new ConflictException("VariantsOnAttributes exist with this id");
        }

        if (variantOnAttributeFoundByVariantIdAttributeId is not null)
        {
            throw new ConflictException(
                "VariantsOnAttributes exist with this variantId and attributeId"
            );
        }

        if (attributeFoundById is null)
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

    public static VariantOnAttributeEntity[] DeleteMany(
        IDeleteOneVariantDto variant,
        IVariantOnAttributeModel[] variantsOnAttributesFoundByVariantId
    )
    {
        return variantsOnAttributesFoundByVariantId
            .Select(variantOnAttribute =>
                DeleteOne(variant: variant, variantOnAttributeFoundByVariantId: variantOnAttribute)
            )
            .ToArray();
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

        if (variantOnAttributeFoundByVariantId.IsDeleted)
        {
            throw new ConflictException("VariantOnAttribute already deleted");
        }

        return new VariantOnAttributeEntity(
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

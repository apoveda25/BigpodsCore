using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Dtos;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Entities;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Dtos;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.Common.Factories;

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
            .ToDictionary(variantOnAttributeModel => variantOnAttributeModel.Id);

        var variantsOnAttributesFoundByVariantIdAttributeIdDict =
            variantsOnAttributesFoundByVariantIdAttributeId
                .AsParallel()
                .ToDictionary(variantOnAttributeModel =>
                    $"{variantOnAttributeModel.VariantId}:{variantOnAttributeModel.AttributeId}"
                );

        var attributesFoundByIdDict = attributesFoundById
            .AsParallel()
            .ToDictionary(attributeModel => attributeModel.Id);

        return variantsOnAttributes
            .AsParallel()
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

        if (attributeFoundById is null)
        {
            throw new NotFoundException("Attributes not found");
        }

        if (attributeFoundById.IsDeleted)
        {
            throw new NotFoundException("Attribute is deleted");
        }

        if (variantOnAttributeFoundByVariantIdAttributeId is not null)
        {
            throw new ConflictException(
                "VariantOnAttribute exist with this variantId and attributeId"
            );
        }

        return new VariantOnAttributeEntity(
            id: variantOnAttribute.Id,
            createdAtDatetime: DateTime.Now,
            createdAtTimezone: variantOnAttribute.CreatedAtTimezone,
            createdBy: variantOnAttribute.CreatedBy,
            variantId: variantOnAttribute.VariantId,
            attributeId: variantOnAttribute.AttributeId
        );
    }

    public static VariantOnAttributeEntity[] DeleteMany(
        IDeleteOneVariantOnAttributeDto[] variantsOnAttributes,
        IVariantOnAttributeModel[] variantsOnAttributesFoundById
    )
    {
        var variantOnAttributesFoundByIdDict = variantsOnAttributesFoundById
            .AsParallel()
            .ToDictionary(variantOnAttributeModel => variantOnAttributeModel.Id);

        return variantsOnAttributes
            .AsParallel()
            .Select(variantOnAttribute =>
                DeleteOne(
                    variantOnAttribute: variantOnAttribute,
                    variantOnAttributeFoundById: variantOnAttributesFoundByIdDict.GetValueOrDefault(
                        variantOnAttribute.Id
                    )
                )
            )
            .ToArray();
    }

    public static VariantOnAttributeEntity DeleteOne(
        IDeleteOneVariantOnAttributeDto variantOnAttribute,
        IVariantOnAttributeModel? variantOnAttributeFoundById
    )
    {
        if (variantOnAttributeFoundById is null)
        {
            throw new NotFoundException("VariantOnAttribute not found");
        }

        return new VariantOnAttributeEntity(
            id: variantOnAttribute.Id,
            isDeleted: true,
            createdAtDatetime: variantOnAttributeFoundById.CreatedAtDatetime,
            updatedAtDatetime: variantOnAttributeFoundById.UpdatedAtDatetime,
            deletedAtDatetime: DateTime.Now,
            createdAtTimezone: variantOnAttributeFoundById.CreatedAtTimezone,
            updatedAtTimezone: variantOnAttributeFoundById.UpdatedAtTimezone,
            deletedAtTimezone: variantOnAttribute.DeletedAtTimezone,
            createdBy: variantOnAttributeFoundById.CreatedBy,
            updatedBy: variantOnAttributeFoundById.UpdatedBy,
            deletedBy: variantOnAttribute.DeletedBy,
            variantId: variantOnAttributeFoundById.VariantId,
            attributeId: variantOnAttributeFoundById.AttributeId
        );
    }
}

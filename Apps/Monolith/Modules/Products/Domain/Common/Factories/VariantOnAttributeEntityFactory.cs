using Bigpods.Monolith.Modules.Products.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Products.Domain.Common.Factories;

public sealed class VariantOnAttributeEntityFactory
{
    public static VariantOnAttributeEntity[] CreateMany(
        ICreateOneVariantOnAttributeDto[] variantsOnAttributes,
        IVariantOnAttributeModel[] variantsOnAttributesFoundById,
        IAttributeModel[] attributesFoundById
    )
    {
        var variantsOnAttributesFoundByIdDict = variantsOnAttributesFoundById
            .AsParallel()
            .ToDictionary(v => v.Id);

        var attributesFoundByIdDict = attributesFoundById.AsParallel().ToDictionary(a => a.Id);

        return variantsOnAttributes
            .Select(variantOnAttribute =>
                CreateOne(
                    variantOnAttribute: variantOnAttribute,
                    variantOnAttributeFoundById: variantsOnAttributesFoundByIdDict.GetValueOrDefault(
                        variantOnAttribute.Id
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

        return new VariantOnAttributeEntity(
            id: variantOnAttribute.Id,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            createdAtTimezone: variantOnAttribute.CreatedAtTimezone,
            createdBy: variantOnAttribute.CreatedBy,
            variantId: variantOnAttribute.VariantId,
            attributeId: variantOnAttribute.AttributeId
        );
    }
}

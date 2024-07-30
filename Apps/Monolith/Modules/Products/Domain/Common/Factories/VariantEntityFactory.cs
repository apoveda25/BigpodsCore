using Bigpods.Monolith.Modules.Products.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Products.Domain.Common.Factories;

public static class VariantEntityFactory
{
    public static VariantEntity[] CreateMany(
        ICreateOneVariantDto[] variants,
        IVariantModel[] variantsFoundById
    )
    {
        var variantsFoundByIdDict = variantsFoundById.AsParallel().ToDictionary(v => v.Id);

        return variants
            .Select(variant =>
                CreateOne(
                    variant: variant,
                    variantFoundById: variantsFoundByIdDict.GetValueOrDefault(variant.Id)
                )
            )
            .ToArray();
    }

    public static VariantEntity CreateOne(
        ICreateOneVariantDto variant,
        IVariantModel? variantFoundById
    )
    {
        if (variantFoundById is not null)
        {
            throw new ConflictException("Variants exist with this id");
        }

        var entity = new VariantEntity(
            id: variant.Id,
            name: variant.Name,
            sku: string.Empty,
            price: variant.Price,
            cost: variant.Cost,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            createdAtTimezone: variant.CreatedAtTimezone,
            createdBy: variant.CreatedBy,
            productId: variant.ProductId
        );

        if (entity.Price.Value <= entity.Cost.Value)
        {
            throw new ConflictException("Price must be greater than cost");
        }

        return entity;
    }
}

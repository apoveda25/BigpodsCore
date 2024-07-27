using Bigpods.Monolith.Modules.Products.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;

namespace Bigpods.Monolith.Modules.Products.Domain.Common.Factories;

public static class ProductAggregateRootFactory
{
    public static ProductAggregateRoot CreateOne(
        ICreateOneProductDto product,
        ICreateOneProductServiceResponse data
    )
    {
        if (data.ProductFoundById is not null)
        {
            throw new ConflictException("Product exist with this id");
        }

        return new ProductAggregateRoot(
            id: product.Id,
            name: product.Name,
            description: product.Description,
            brand: product.Brand,
            model: product.Model,
            isCompleted: false,
            isPublished: false,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: product.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: product.CreatedBy,
            updatedBy: null,
            deletedBy: null
        );
    }

    public static ProductAggregateRoot UpdateOne(
        IUpdateOneProductDto product,
        IUpdateOneProductServiceResponse data
    )
    {
        if (data.ProductFoundById is null)
        {
            throw new NotFoundException("Product does not exist with this id");
        }

        if (data.ProductFoundById.IsDeleted)
        {
            throw new ConflictException("Product is deleted");
        }

        if (data.ProductFoundById.Id != product.Id)
        {
            throw new ConflictException("Product id does not match");
        }

        return new ProductAggregateRoot(
            id: data.ProductFoundById.Id,
            name: product.Name ?? data.ProductFoundById.Name,
            description: product.Description ?? data.ProductFoundById.Description,
            brand: product.Brand ?? data.ProductFoundById.Brand,
            model: product.Model ?? data.ProductFoundById.Model,
            isCompleted: data.ProductFoundById.IsCompleted,
            isPublished: data.ProductFoundById.IsPublished,
            isDeleted: data.ProductFoundById.IsDeleted,
            createdAtDatetime: data.ProductFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: data.ProductFoundById.DeletedAtDatetime,
            createdAtTimezone: data.ProductFoundById.CreatedAtTimezone,
            updatedAtTimezone: product.UpdatedAtTimezone,
            deletedAtTimezone: data.ProductFoundById.DeletedAtTimezone,
            createdBy: data.ProductFoundById.CreatedBy,
            updatedBy: product.UpdatedBy,
            deletedBy: data.ProductFoundById.DeletedBy
        );
    }
}

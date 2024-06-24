using Bigpods.Monolith.Modules.Products.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Products.Domain.Common.Aggregates;

public sealed class ProductAggregateRoot
{
    public Guid Id { get; private set; }
    public NameVO Name { get; private set; }
    public SentenceVO Description { get; private set; }
    public BrandVO Brand { get; private set; }
    public ModelVO Model { get; private set; }
    public StockVO Stock { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsPublished { get; private set; }
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
    public VariantEntity[] Variants { get; private set; }

    private ProductAggregateRoot(
        Guid id,
        string name,
        string description,
        string brand,
        string model,
        int stock,
        bool isCompleted,
        bool isPublished,
        bool isDeleted,
        DateTime createdAtDatetime,
        DateTime? updatedAtDatetime,
        DateTime? deletedAtDatetime,
        string createdAtTimezone,
        string? updatedAtTimezone,
        string? deletedAtTimezone,
        Guid createdBy,
        Guid? updatedBy,
        Guid? deletedBy
    )
    {
        Id = id;
        Name = new NameVO(name);
        Description = new SentenceVO(description);
        Brand = new BrandVO(brand);
        Model = new ModelVO(model);
        Stock = new StockVO(stock);
        IsCompleted = isCompleted;
        IsPublished = isPublished;
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
        Variants = [];
    }

    public static ProductAggregateRoot CreateOne(
        ICreateOneProductDto product,
        IProductModel? productFoundById
    )
    {
        if (productFoundById is not null)
        {
            throw new ConflictException("Product exist with this id");
        }

        return new ProductAggregateRoot(
            id: product.Id,
            name: product.Name,
            description: product.Description,
            brand: product.Brand,
            model: product.Model,
            stock: 0,
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

    public void AttachVariants(VariantEntity[] variants)
    {
        foreach (var variant in variants)
        {
            if (IsVariantExist(variant.Id))
            {
                throw new ConflictException("Variant exist with this id");
            }

            if (!VariantBelongToProduct(variant.ProductId))
            {
                throw new ConflictException("Variant not belong to this product");
            }
        }

        Variants = [.. Variants, .. variants];

        AddStocks();
    }

    private bool IsVariantExist(Guid variantId)
    {
        return Variants.Any(variant => variant.Id == variantId);
    }

    private bool VariantBelongToProduct(Guid productId)
    {
        return productId == Id;
    }

    private void AddStocks()
    {
        Stock = new StockVO(Variants.Sum(variant => variant.Stock.Value));
    }

    public void AttachVariantsOnAttributes(VariantOnAttributeEntity[] variantsOnAttributes)
    {
        foreach (var variantOnAttribute in variantsOnAttributes)
        {
            var variant = Variants.FirstOrDefault(variant => variant.Id == variantOnAttribute.VariantId);

            if (variant is null)
            {
                throw new NotFoundException("Variant not found");
            }

            variant.AttachVariantOnAttribute(variantOnAttribute);
        }
    }

    public static ProductAggregateRoot UpdateOne(
        IUpdateOneProductDto product,
        IProductModel? productFoundById
    )
    {
        if (productFoundById is null)
        {
            throw new NotFoundException("Product does not exist with this id");
        }

        if (productFoundById.IsDeleted)
        {
            throw new ConflictException("Product is deleted");
        }

        if (productFoundById.Id != product.Id)
        {
            throw new ConflictException("Product id does not match");
        }

        return new ProductAggregateRoot(
            id: productFoundById.Id,
            name: product.Name ?? productFoundById.Name,
            description: product.Description ?? productFoundById.Description,
            brand: product.Brand ?? productFoundById.Brand,
            model: product.Model ?? productFoundById.Model,
            stock: productFoundById.Stock,
            isCompleted: productFoundById.IsCompleted,
            isPublished: productFoundById.IsPublished,
            isDeleted: productFoundById.IsDeleted,
            createdAtDatetime: productFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: productFoundById.DeletedAtDatetime,
            createdAtTimezone: productFoundById.CreatedAtTimezone,
            updatedAtTimezone: product.UpdatedAtTimezone,
            deletedAtTimezone: productFoundById.DeletedAtTimezone,
            createdBy: productFoundById.CreatedBy,
            updatedBy: product.UpdatedBy,
            deletedBy: productFoundById.DeletedBy
        );
    }
}

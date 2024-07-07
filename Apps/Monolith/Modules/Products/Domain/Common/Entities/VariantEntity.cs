using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Products.Domain.Common.Entities;

public sealed class VariantEntity
{
    public Guid Id { get; private set; }
    public NameVO Name { get; private set; }
    public string Sku { get; private set; }
    public PriceVO Price { get; private set; }
    public CostVO Cost { get; private set; }
    public StockVO Stock { get; private set; }
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
    public Guid ProductId { get; private set; }
    public VariantOnAttributeEntity[] VariantsOnAttributes { get; private set; }

    private VariantEntity(
        Guid id,
        string name,
        string sku,
        float price,
        float cost,
        int stock,
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
        Guid productId
    )
    {
        Id = id;
        Name = new NameVO(name);
        Sku = sku;
        Price = new PriceVO(price);
        Cost = new CostVO(cost);
        Stock = new StockVO(stock);
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
        ProductId = productId;
        VariantsOnAttributes = [];
    }

    public static VariantEntity CreateOne(
        ICreateOneVariantDto variant,
        IVariantModel[] variantsFoundById
    )
    {
        if (variantsFoundById.Length != 0)
        {
            throw new ConflictException("Variants exist with this id");
        }

        var entity = new VariantEntity
        (
            id: variant.Id,
            name: variant.Name,
            sku: string.Empty,
            price: variant.Price,
            cost: variant.Cost,
            stock: 0,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: variant.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: variant.CreatedBy,
            updatedBy: null,
            deletedBy: null,
            productId: variant.ProductId
        );

        if (entity.Price.Value <= entity.Cost.Value)
        {
            throw new ConflictException("Price must be greater than cost");
        }

        return entity;
    }

    public void AttachVariantOnAttribute(VariantOnAttributeEntity variantOnAttribute)
    {
        if (IsVariantOnAttributeExist(variantOnAttribute))
        {
            throw new ConflictException("VariantOnAttribute exist with this id or attributeId");
        }

        if (!VariantOnAttributeBelongToVariant(variantOnAttribute.VariantId))
        {
            throw new ConflictException("VariantOnAttribute not belong to this variant");
        }

        VariantsOnAttributes = [.. VariantsOnAttributes, variantOnAttribute];
    }

    private bool IsVariantOnAttributeExist(VariantOnAttributeEntity entity)
    {
        return VariantsOnAttributes.Any(entity.IsEquals);
    }

    private bool VariantOnAttributeBelongToVariant(Guid variantId)
    {
        return variantId == Id;
    }
}

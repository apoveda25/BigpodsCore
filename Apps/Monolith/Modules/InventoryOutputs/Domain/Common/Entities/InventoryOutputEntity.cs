using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Entities;

public sealed class InventoryOutputEntity
{
    public Guid Id { get; private set; }
    public StockVO Stock { get; private set; }
    public SentenceVO Comment { get; private set; }
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
    public Guid VariantId { get; private set; }
    public Guid WarehouseId { get; private set; }
    public Guid InventoryId { get; private set; }

    private InventoryOutputEntity(
        Guid id,
        int stock,
        string comment,
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
        Guid productId,
        Guid variantId,
        Guid warehouseId,
        Guid inventoryId
    )
    {
        Id = id;
        Stock = new StockVO(stock);
        Comment = new SentenceVO(comment);
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
        VariantId = variantId;
        WarehouseId = warehouseId;
        InventoryId = inventoryId;
    }

    public static InventoryOutputEntity CreateOne(
        ICreateOneInventoryOutputDto inventoryOutput,
        IInventoryOutputModel? inventoryOutputFoundById,
        IInventoryModel? inventoryFoundById,
        IProductModel? productFoundById,
        IVariantModel? variantFoundById,
        IWarehouseModel? warehouseFoundById
    )
    {
        if (inventoryFoundById is null)
        {
            throw new NotFoundException("Inventory not found");
        }

        if (productFoundById is null)
        {
            throw new NotFoundException("Product not found");
        }

        if (variantFoundById is null)
        {
            throw new NotFoundException("Variant not found");
        }

        if (warehouseFoundById is null)
        {
            throw new NotFoundException("Warehouse not found");
        }

        if (inventoryOutputFoundById is not null)
        {
            throw new ConflictException("Inventory output exist with this id");
        }

        return new InventoryOutputEntity(
            id: inventoryOutput.Id,
            stock: inventoryOutput.Stock,
            comment: inventoryOutput.Comment,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: inventoryOutput.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: inventoryOutput.CreatedBy,
            updatedBy: null,
            deletedBy: null,
            productId: inventoryOutput.ProductId,
            variantId: inventoryOutput.VariantId,
            warehouseId: inventoryOutput.WarehouseId,
            inventoryId: inventoryOutput.InventoryId
        );
    }
}

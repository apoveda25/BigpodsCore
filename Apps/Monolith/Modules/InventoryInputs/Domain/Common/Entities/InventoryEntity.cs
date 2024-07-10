using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Entities;

public sealed class InventoryEntity
{
    public Guid Id { get; private set; }
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
    public Guid VariantId { get; private set; }
    public Guid WarehouseId { get; private set; }

    private InventoryEntity(
        Guid id,
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
        Guid productId,
        Guid variantId,
        Guid warehouseId
    )
    {
        Id = id;
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
        VariantId = variantId;
        WarehouseId = warehouseId;
    }

    public static InventoryEntity BuildOne(
        IInventoryModel? inventoryInput
    )
    {
        if (inventoryInput is null)
        {
            throw new NotFoundException("Inventory does not exist with this id");
        }

        if (inventoryInput.IsDeleted)
        {
            throw new ConflictException("Inventory is deleted");
        }

        return new InventoryEntity(
            id: inventoryInput.Id,
            stock: inventoryInput.Stock,
            isDeleted: inventoryInput.IsDeleted,
            createdAtDatetime: inventoryInput.CreatedAtDatetime,
            updatedAtDatetime: inventoryInput.UpdatedAtDatetime,
            deletedAtDatetime: inventoryInput.DeletedAtDatetime,
            createdAtTimezone: inventoryInput.CreatedAtTimezone,
            updatedAtTimezone: inventoryInput.UpdatedAtTimezone,
            deletedAtTimezone: inventoryInput.DeletedAtTimezone,
            createdBy: inventoryInput.CreatedBy,
            updatedBy: inventoryInput.UpdatedBy,
            deletedBy: inventoryInput.DeletedBy,
            productId: inventoryInput.ProductId,
            variantId: inventoryInput.VariantId,
            warehouseId: inventoryInput.WarehouseId
        );
    }

    public void AddStock(StockVO stockInput)
    {
        Stock = new StockVO(Stock.Value + stockInput.Value);
    }
}

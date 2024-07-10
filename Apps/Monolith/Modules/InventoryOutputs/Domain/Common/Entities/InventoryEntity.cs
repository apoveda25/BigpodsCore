using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Entities;

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
        IInventoryModel? inventoryOutput
    )
    {
        if (inventoryOutput is null)
        {
            throw new NotFoundException("Inventory does not exist with this id");
        }

        if (inventoryOutput.IsDeleted)
        {
            throw new ConflictException("Inventory is deleted");
        }

        return new InventoryEntity(
            id: inventoryOutput.Id,
            stock: inventoryOutput.Stock,
            isDeleted: inventoryOutput.IsDeleted,
            createdAtDatetime: inventoryOutput.CreatedAtDatetime,
            updatedAtDatetime: inventoryOutput.UpdatedAtDatetime,
            deletedAtDatetime: inventoryOutput.DeletedAtDatetime,
            createdAtTimezone: inventoryOutput.CreatedAtTimezone,
            updatedAtTimezone: inventoryOutput.UpdatedAtTimezone,
            deletedAtTimezone: inventoryOutput.DeletedAtTimezone,
            createdBy: inventoryOutput.CreatedBy,
            updatedBy: inventoryOutput.UpdatedBy,
            deletedBy: inventoryOutput.DeletedBy,
            productId: inventoryOutput.ProductId,
            variantId: inventoryOutput.VariantId,
            warehouseId: inventoryOutput.WarehouseId
        );
    }

    public void SubtractStock(StockVO stockOutput)
    {
        if (stockOutput.Value > Stock.Value)
        {
            throw new ConflictException("Stock can not be subtracted");
        }

        Stock = new StockVO(Stock.Value - stockOutput.Value);
    }
}

using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Entities;

public sealed class InventoryEntity(
    Guid? id = null,
    int? stock = null,
    bool? isDeleted = null,
    DateTime? createdAtDatetime = null,
    DateTime? updatedAtDatetime = null,
    DateTime? deletedAtDatetime = null,
    string? createdAtTimezone = null,
    string? updatedAtTimezone = null,
    string? deletedAtTimezone = null,
    Guid? createdBy = null,
    Guid? updatedBy = null,
    Guid? deletedBy = null,
    Guid? productId = null,
    Guid? variantId = null,
    Guid? warehouseId = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public StockVO Stock { get; private set; } = new StockVO(stock ?? 0);
    public bool IsDeleted { get; private set; } = isDeleted ?? false;
    public DateTime CreatedAtDatetime { get; private set; } = createdAtDatetime ?? DateTime.Now;
    public DateTime? UpdatedAtDatetime { get; private set; } = updatedAtDatetime;
    public DateTime? DeletedAtDatetime { get; private set; } = deletedAtDatetime;
    public string CreatedAtTimezone { get; private set; } = createdAtTimezone ?? string.Empty;
    public string? UpdatedAtTimezone { get; private set; } = updatedAtTimezone;
    public string? DeletedAtTimezone { get; private set; } = deletedAtTimezone;
    public Guid CreatedBy { get; private set; } = createdBy ?? Guid.Empty;
    public Guid? UpdatedBy { get; private set; } = updatedBy;
    public Guid? DeletedBy { get; private set; } = deletedBy;
    public Guid ProductId { get; private set; } = productId ?? Guid.Empty;
    public Guid VariantId { get; private set; } = variantId ?? Guid.Empty;
    public Guid WarehouseId { get; private set; } = warehouseId ?? Guid.Empty;

    public void SubtractStock(StockVO stockOutput)
    {
        if (stockOutput.Value > Stock.Value)
        {
            throw new ConflictException("Stock can not be subtracted");
        }

        Stock = new StockVO(Stock.Value - stockOutput.Value);
    }

    public bool IsEqual(InventoryEntity inventoryEntity)
    {
        return inventoryEntity.Id == Id
            || (
                inventoryEntity.VariantId == VariantId && inventoryEntity.WarehouseId == WarehouseId
            );
    }
}

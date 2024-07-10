using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Entities;

public sealed class InventoryInputEntity(
    Guid? id = null,
    int? stock = null,
    string? comment = null,
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
    Guid? warehouseId = null,
    Guid? inventoryId = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public StockVO Stock { get; private set; } = new StockVO(stock ?? 0);
    public SentenceVO Comment { get; private set; } = new SentenceVO(comment ?? string.Empty);
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
    public Guid InventoryId { get; private set; } = inventoryId ?? Guid.Empty;

    public bool IsEqual(InventoryInputEntity inventoryInput)
    {
        return Id == inventoryInput.Id;
    }
}
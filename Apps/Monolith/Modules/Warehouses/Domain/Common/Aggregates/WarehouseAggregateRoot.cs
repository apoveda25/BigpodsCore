using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;
using Bigpods.Monolith.Modules.Warehouses.Domain.Common.Factories;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Services;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.Common.Aggregates;

public sealed class WarehouseAggregateRoot(
    Guid? id = null,
    string? name = null,
    string? description = null,
    bool? isDeleted = null,
    DateTime? createdAtDatetime = null,
    DateTime? updatedAtDatetime = null,
    DateTime? deletedAtDatetime = null,
    string? createdAtTimezone = null,
    string? updatedAtTimezone = null,
    string? deletedAtTimezone = null,
    Guid? createdBy = null,
    Guid? updatedBy = null,
    Guid? deletedBy = null
)
{
    public Guid Id { get; private set; } = id ?? Guid.NewGuid();
    public NameVO Name { get; private set; } = new NameVO(name ?? string.Empty);
    public SentenceVO Description { get; private set; } =
        new SentenceVO(description ?? string.Empty);
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

    public static WarehouseAggregateRoot CreateOne(
        ICreateOneWarehouseCommand command,
        ICreateOneWarehouseServiceResponse data
    )
    {
        return WarehouseAggregateRootFactory.CreateOne(
            warehouse: command.WarehouseDto,
            warehouseFoundById: data.WarehouseFoundById,
            warehouseFoundByName: data.WarehouseFoundByName
        );
    }

    public static WarehouseAggregateRoot UpdateOne(
        IUpdateOneWarehouseCommand command,
        IUpdateOneWarehouseServiceResponse data
    )
    {
        return WarehouseAggregateRootFactory.UpdateOne(
            warehouse: command.WarehouseDto,
            warehouseFoundById: data.WarehouseFoundById,
            warehouseFoundByName: data.WarehouseFoundByName
        );
    }

    public static WarehouseAggregateRoot DeleteOne(
        IDeleteOneWarehouseCommand command,
        IDeleteOneWarehouseServiceResponse data
    )
    {
        return WarehouseAggregateRootFactory.DeleteOne(
            warehouse: command.WarehouseDto,
            warehouseFoundById: data.WarehouseFoundById,
            inventoriesFoundByWarehouseId: data.InventoriesFoundByWarehouseId,
            inventoryInputsFoundByWarehouseId: data.InventoryInputsFoundByWarehouseId,
            inventoryOutputsFoundByWarehouseId: data.InventoryOutputsFoundByWarehouseId
        );
    }
}

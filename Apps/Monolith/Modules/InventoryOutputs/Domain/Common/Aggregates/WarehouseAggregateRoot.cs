using Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Factories;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Aggregates;

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
    public InventoryEntity[] Inventories { get; private set; } = [];
    public InventoryOutputEntity[] InventoryOutputs { get; private set; } = [];

    public static WarehouseAggregateRoot CreateOneInventoryOutput(
        ICreateOneInventoryOutputCommand command,
        ICreateOneInventoryOutputServiceResponse data
    )
    {
        var aggregateRoot = WarehouseAggregateRootFactory.BuildOne(data.WarehouseFoundById);

        var inventoryEntity = InventoryEntityFactory.BuildOne(
            inventoryOutput: data.InventoryFoundById
        );

        var inventoryOutputEntity = InventoryOutputEntityFactory.CreateOne(
            inventoryOutput: command.InventoryOutputDto,
            inventoryOutputFoundById: data.InventoryOutputFoundById,
            inventoryFoundById: data.InventoryFoundById,
            productFoundById: data.ProductFoundById,
            variantFoundById: data.VariantFoundById,
            warehouseFoundById: data.WarehouseFoundById
        );

        inventoryEntity.SubtractStock(inventoryOutputEntity.Stock);

        aggregateRoot.AttachOneInventory(inventoryEntity);

        aggregateRoot.AttachOneInventoryOutput(inventoryOutputEntity);

        return aggregateRoot;
    }

    private void AttachOneInventory(InventoryEntity inventory)
    {
        if (inventory.IsDeleted)
        {
            throw new ConflictException("Inventory deleted can not be attached");
        }

        if (IsInventoryAttach(inventory))
        {
            throw new ConflictException("Inventory attached with this id or variant id");
        }

        if (IsNotInventoryBelongToWarehouse(inventory))
        {
            throw new ConflictException("Inventory not belong to this warehouse");
        }

        Inventories = [.. Inventories, inventory];
    }

    private void AttachOneInventoryOutput(InventoryOutputEntity inventoryOutput)
    {
        if (inventoryOutput.IsDeleted)
        {
            throw new ConflictException("Inventory input deleted can not be attached");
        }

        if (IsInventoryOutputAttach(inventoryOutput))
        {
            throw new ConflictException("Inventory input attached with this id or variant id");
        }

        if (IsNotInventoryOutputBelongToWarehouse(inventoryOutput))
        {
            throw new ConflictException("Inventory input not belong to this warehouse");
        }

        InventoryOutputs = [.. InventoryOutputs, inventoryOutput];
    }

    private bool IsInventoryAttach(InventoryEntity inventory)
    {
        return Inventories.Any(inventory.IsEqual);
    }

    private bool IsNotInventoryBelongToWarehouse(InventoryEntity inventory)
    {
        return inventory.WarehouseId != Id;
    }

    private bool IsInventoryOutputAttach(InventoryOutputEntity inventoryOutput)
    {
        return InventoryOutputs.Any(inventoryOutput.IsEqual);
    }

    private bool IsNotInventoryOutputBelongToWarehouse(InventoryOutputEntity inventoryOutput)
    {
        return inventoryOutput.WarehouseId != Id;
    }
}

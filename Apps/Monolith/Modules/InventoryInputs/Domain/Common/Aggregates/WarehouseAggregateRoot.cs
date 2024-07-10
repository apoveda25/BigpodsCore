using Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Factories;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;
using Humanizer;

namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Aggregates;

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
    public InventoryInputEntity[] InventoryInputs { get; private set; } = [];

    public static WarehouseAggregateRoot CreateOneInventoryInput(
        ICreateOneInventoryInputCommand command,
        ICreateOneInventoryInputServiceResponse data
    )
    {
        var aggregateRoot = WarehouseAggregateRootFactory.BuildOne(data.WarehouseFoundById);

        var inventoryEntity = InventoryEntityFactory.BuildOne(
            inventoryInput: data.InventoryFoundById
        );

        var inventoryInputEntity = InventoryInputEntityFactory.CreateOne(
            inventoryInput: command.InventoryInputDto,
            inventoryInputFoundById: data.InventoryInputFoundById,
            inventoryFoundById: data.InventoryFoundById,
            productFoundById: data.ProductFoundById,
            variantFoundById: data.VariantFoundById,
            warehouseFoundById: data.WarehouseFoundById
        );

        inventoryEntity.AddStock(inventoryInputEntity.Stock);

        aggregateRoot.AttachOneInventory(inventoryEntity);

        aggregateRoot.AttachOneInventoryInput(inventoryInputEntity);

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

    private void AttachOneInventoryInput(InventoryInputEntity inventoryInput)
    {
        if (inventoryInput.IsDeleted)
        {
            throw new ConflictException("Inventory input deleted can not be attached");
        }

        if (IsInventoryInputAttach(inventoryInput))
        {
            throw new ConflictException("Inventory input attached with this id or variant id");
        }

        if (IsNotInventoryInputBelongToWarehouse(inventoryInput))
        {
            throw new ConflictException("Inventory input not belong to this warehouse");
        }

        InventoryInputs = [.. InventoryInputs, inventoryInput];
    }

    private bool IsInventoryAttach(InventoryEntity inventory)
    {
        return Inventories.Any(inventory.IsEqual);
    }

    private bool IsNotInventoryBelongToWarehouse(InventoryEntity inventory)
    {
        return inventory.WarehouseId != Id;
    }

    private bool IsInventoryInputAttach(InventoryInputEntity inventoryInput)
    {
        return InventoryInputs.Any(inventoryInput.IsEqual);
    }

    private bool IsNotInventoryInputBelongToWarehouse(InventoryInputEntity inventoryInput)
    {
        return inventoryInput.WarehouseId != Id;
    }
}

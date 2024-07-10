using Bigpods.Monolith.Modules.Inventories.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Inventories.Domain.Common.Factories;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Inventories.Domain.Common.Aggregates;

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

    public static WarehouseAggregateRoot CreateOneInventory(
        ICreateOneInventoryCommand command,
        ICreateOneInventoryServiceResponse data
    )
    {
        var aggregateRoot = WarehouseAggregateRootFactory.BuildOne(data.WarehouseFoundById);

        var inventoryEntity = InventoryEntityFactory.CreateOne(
            inventory: command.InventoryDto,
            inventoryFoundById: data.InventoryFoundById,
            inventoryFoundByWarehouseIdVariantId: data.InventoryFoundByWarehouseIdVariantId,
            productFoundById: data.ProductFoundById,
            variantFoundById: data.VariantFoundById,
            warehouseFoundById: data.WarehouseFoundById
        );

        aggregateRoot.AttachOneInventory(inventoryEntity);

        return aggregateRoot;
    }

    public static WarehouseAggregateRoot DeleteOneInventory(
        IDeleteOneInventoryCommand command,
        IDeleteOneInventoryServiceResponse data
    )
    {
        var aggregateRoot = WarehouseAggregateRootFactory.BuildOne(data.WarehouseFoundById);

        var inventoryEntity = InventoryEntityFactory.DeleteOne(
            inventory: command.InventoryDto,
            inventoryFoundById: data.InventoryFoundById,
            inventoryInputsFoundByInventoryId: data.InventoryInputsFoundByInventoryId,
            inventoryOutputsFoundByInventoryId: data.InventoryOutputsFoundByInventoryId
        );

        aggregateRoot.DettachOneInventory(inventoryEntity);

        return aggregateRoot;
    }

    private void AttachOneInventory(InventoryEntity inventory)
    {
        if (inventory.IsDeleted)
        {
            throw new ConflictException("Inventory deleted cannot be attached");
        }

        if (IsInventoryExist(inventory))
        {
            throw new ConflictException("Inventory attached with this id or variant id");
        }

        if (IsNotInventoryBelongToWarehouse(inventory))
        {
            throw new ConflictException("Inventory not belong to this warehouse");
        }

        Inventories = [.. Inventories, inventory];
    }

    private bool IsInventoryExist(InventoryEntity inventory)
    {
        return Inventories.Any(inventory.IsEqual);
    }

    private bool IsNotInventoryBelongToWarehouse(InventoryEntity inventory)
    {
        return inventory.WarehouseId != Id;
    }

    private void DettachOneInventory(InventoryEntity inventory)
    {
        if (!inventory.IsDeleted)
        {
            throw new ConflictException("Inventory not deleted can not be dettached");
        }

        if (IsInventoryExist(inventory))
        {
            throw new NotFoundException("Inventory dettached");
        }

        if (IsNotInventoryBelongToWarehouse(inventory))
        {
            throw new ConflictException("Inventory not belong to this warehouse");
        }

        Inventories = [.. Inventories, inventory];
    }
}

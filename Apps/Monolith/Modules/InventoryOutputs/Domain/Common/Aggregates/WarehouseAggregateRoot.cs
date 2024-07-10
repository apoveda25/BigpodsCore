using Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Aggregates;

public sealed class WarehouseAggregateRoot
{
    public Guid Id { get; private set; }
    public NameVO Name { get; private set; }
    public SentenceVO Description { get; private set; }
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
    public InventoryEntity[] Inventories { get; private set; }
    public InventoryOutputEntity[] InventoryOutputs { get; private set; }

    private WarehouseAggregateRoot(
        Guid id,
        string name,
        string description,
        bool isDeleted,
        DateTime createdAtDatetime,
        DateTime? updatedAtDatetime,
        DateTime? deletedAtDatetime,
        string createdAtTimezone,
        string? updatedAtTimezone,
        string? deletedAtTimezone,
        Guid createdBy,
        Guid? updatedBy,
        Guid? deletedBy
    )
    {
        Id = id;
        Name = new NameVO(name);
        Description = new SentenceVO(description);
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
        Inventories = [];
        InventoryOutputs = [];
    }

    public static WarehouseAggregateRoot CreateOneInventoryOutput(
        ICreateOneInventoryOutputDto inventoryOutput,
        ICreateOneInventoryOutputServiceResponse data
    )
    {
        var aggregateRoot = BuildOne(data.WarehouseFoundById);

        var inventoryEntity = InventoryEntity.BuildOne(inventoryOutput: data.InventoryFoundById);

        var inventoryOutputEntity = InventoryOutputEntity.CreateOne(
            inventoryOutput: inventoryOutput,
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

    private static WarehouseAggregateRoot BuildOne(
        IWarehouseModel? warehouse
    )
    {
        if (warehouse is null)
        {
            throw new NotFoundException("Warehouse not exist with this id");
        }

        return new WarehouseAggregateRoot(
            id: warehouse.Id,
            name: warehouse.Name,
            description: warehouse.Description,
            isDeleted: warehouse.IsDeleted,
            createdAtDatetime: warehouse.CreatedAtDatetime,
            updatedAtDatetime: warehouse.UpdatedAtDatetime,
            deletedAtDatetime: warehouse.DeletedAtDatetime,
            createdAtTimezone: warehouse.CreatedAtTimezone,
            updatedAtTimezone: warehouse.UpdatedAtTimezone,
            deletedAtTimezone: warehouse.DeletedAtTimezone,
            createdBy: warehouse.CreatedBy,
            updatedBy: warehouse.UpdatedBy,
            deletedBy: warehouse.DeletedBy
        );
    }

    private void AttachOneInventory(InventoryEntity inventory)
    {
        if (inventory.IsDeleted)
        {
            throw new ConflictException("Inventory deleted can not be attached");
        }

        if (IsInventoryAttach(inventoryId: inventory.Id, variantId: inventory.VariantId))
        {
            throw new ConflictException("Inventory attached with this id or variant id");
        }

        if (IsNotInventoryBelongToWarehouse(warehouseId: inventory.WarehouseId))
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

        if (IsInventoryOutputAttach(inventoryOutputId: inventoryOutput.Id))
        {
            throw new ConflictException("Inventory input attached with this id or variant id");
        }

        if (IsNotInventoryOutputBelongToWarehouse(warehouseId: inventoryOutput.WarehouseId))
        {
            throw new ConflictException("Inventory input not belong to this warehouse");
        }

        InventoryOutputs = [.. InventoryOutputs, inventoryOutput];
    }

    private bool IsInventoryAttach(Guid inventoryId, Guid variantId)
    {
        return Inventories.Any(inventory => inventory.Id == inventoryId || inventory.VariantId == variantId);
    }

    private bool IsNotInventoryBelongToWarehouse(Guid warehouseId)
    {
        return warehouseId != Id;
    }

    private bool IsInventoryOutputAttach(Guid inventoryOutputId)
    {
        return InventoryOutputs.Any(inventoryOutput => inventoryOutput.Id == inventoryOutputId);
    }

    private bool IsNotInventoryOutputBelongToWarehouse(Guid warehouseId)
    {
        return warehouseId != Id;
    }
}

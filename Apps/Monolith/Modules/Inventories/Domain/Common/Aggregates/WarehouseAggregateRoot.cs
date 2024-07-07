using Bigpods.Monolith.Modules.Inventories.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Inventories.Domain.Common.Aggregates;

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
    }

    public InventoryEntity CreateOneVariant(
        ICreateOneInventoryDto inventory,
        IInventoryModel? inventoryFoundById,
        IProductModel? productFoundById,
        IVariantModel? variantFoundById,
        IWarehouseModel? warehouseFoundById,
        IInventoryModel[] inventoriesFoundByProductIdWarehouseId
    )
    {
        AttachInventories(inventoriesFoundByProductIdWarehouseId.Select(InventoryEntity.Build).ToArray());

        var inventoryEntity = InventoryEntity.CreateOne(
            inventory: inventory,
            inventoryFoundById: inventoryFoundById,
            productFoundById: productFoundById,
            variantFoundById: variantFoundById,
            warehouseFoundById: warehouseFoundById
        );

        AttachInventories([inventoryEntity]);

        return inventoryEntity;
    }

    public InventoryEntity DeleteOneVariant(
        IDeleteOneInventoryDto inventory,
        IInventoryModel? inventoryFoundById,
        IInventoryModel[] inventoriesFoundByProductIdWarehouseId,
        IInventoryInputModel[] inventoryInputsFoundByInventoryId,
        IInventoryOutputModel[] inventoryOutputsFoundByInventoryId
    )
    {
        AttachInventories(inventoriesFoundByProductIdWarehouseId.Select(InventoryEntity.Build).ToArray());

        var inventoryEntity = InventoryEntity.DeleteOne(
            inventory: inventory,
            inventoryFoundById: inventoryFoundById,
            inventoryInputsFoundByInventoryId: inventoryInputsFoundByInventoryId,
            inventoryOutputsFoundByInventoryId: inventoryOutputsFoundByInventoryId
        );

        DettachInventories([inventoryEntity]);

        return inventoryEntity;
    }

    public static WarehouseAggregateRoot Build(
        IWarehouseModel? warehouse
    )
    {
        if (warehouse is null)
        {
            throw new NotFoundException("Warehouse does not exist with this id");
        }

        if (warehouse.IsDeleted)
        {
            throw new ConflictException("Warehouse is deleted");
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

    private void AttachInventories(InventoryEntity[] inventories)
    {
        foreach (var inventory in inventories)
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
        }

        Inventories = [.. Inventories, .. inventories];
    }

    private bool IsInventoryAttach(Guid inventoryId, Guid variantId)
    {
        return Inventories.Any(inventory => inventory.Id == inventoryId || inventory.VariantId == variantId);
    }

    private bool IsNotInventoryBelongToWarehouse(Guid warehouseId)
    {
        return warehouseId != Id;
    }

    private void DettachInventories(InventoryEntity[] inventories)
    {
        foreach (var inventory in inventories)
        {
            if (!inventory.IsDeleted)
            {
                throw new ConflictException("Inventory not deleted can not be dettached");
            }

            if (IsInventoryDettached(inventoryId: inventory.Id))
            {
                throw new NotFoundException("Inventory not attached for dettach");
            }

            if (IsNotInventoryBelongToWarehouse(warehouseId: inventory.WarehouseId))
            {
                throw new ConflictException("Inventory not belong to this warehouse");
            }
        }

        Inventories = Inventories.SkipWhile(inventory => inventories.Any(inventoryToDetach => inventory.Id == inventoryToDetach.Id)).ToArray();
    }

    private bool IsInventoryDettached(Guid inventoryId)
    {
        return !Inventories.Any(inventory => inventory.Id == inventoryId);
    }
}

using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;

namespace Bigpods.Monolith.Modules.Inventories.Domain.Common.Entities;

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

    public static InventoryEntity CreateOne(
        ICreateOneInventoryDto inventory,
        IInventoryModel? inventoryFoundById,
        IProductModel? productFoundById,
        IVariantModel? variantFoundById,
        IWarehouseModel? warehouseFoundById
    )
    {
        if (inventoryFoundById is not null)
        {
            throw new ConflictException("Inventory exist with this id");
        }

        if (productFoundById is null)
        {
            throw new NotFoundException("Product not found");
        }

        if (variantFoundById is null)
        {
            throw new NotFoundException("Variant not found");
        }

        if (warehouseFoundById is null)
        {
            throw new NotFoundException("Warehouse not found");
        }

        return new InventoryEntity
        (
            id: inventory.Id,
            stock: 0,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: inventory.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: inventory.CreatedBy,
            updatedBy: null,
            deletedBy: null,
            productId: inventory.ProductId,
            variantId: inventory.VariantId,
            warehouseId: inventory.WarehouseId
        );
    }

    public static InventoryEntity DeleteOne(
        IDeleteOneInventoryDto inventory,
        IInventoryModel? inventoryFoundById,
        IInventoryInputModel[] inventoryInputsFoundByInventoryId,
        IInventoryOutputModel[] inventoryOutputsFoundByInventoryId
    )
    {
        if (inventoryFoundById is null)
        {
            throw new NotFoundException("Inventory not found");
        }

        if (inventoryFoundById.IsDeleted)
        {
            throw new ConflictException("Inventory is deleted");
        }

        if (inventoryInputsFoundByInventoryId.Length > 0)
        {
            throw new ConflictException("Inventory with inputs can not be deleted");
        }

        if (inventoryOutputsFoundByInventoryId.Length > 0)
        {
            throw new ConflictException("Inventory with outputs can not be deleted");
        }

        return new InventoryEntity
        (
            id: inventoryFoundById.Id,
            stock: inventoryFoundById.Stock,
            isDeleted: true,
            createdAtDatetime: inventoryFoundById.CreatedAtDatetime,
            updatedAtDatetime: inventoryFoundById.UpdatedAtDatetime,
            deletedAtDatetime: DateTime.Now,
            createdAtTimezone: inventoryFoundById.CreatedAtTimezone,
            updatedAtTimezone: inventoryFoundById.UpdatedAtTimezone,
            deletedAtTimezone: inventory.DeletedAtTimezone,
            createdBy: inventoryFoundById.CreatedBy,
            updatedBy: inventoryFoundById.UpdatedBy,
            deletedBy: inventory.DeletedBy,
            productId: inventoryFoundById.ProductId,
            variantId: inventoryFoundById.VariantId,
            warehouseId: inventoryFoundById.WarehouseId
        );
    }

    public static InventoryEntity Build(
        IInventoryModel? inventory
    )
    {
        if (inventory is null)
        {
            throw new NotFoundException("Inventory does not exist with this id");
        }

        if (inventory.IsDeleted)
        {
            throw new ConflictException("Inventory is deleted");
        }

        return new InventoryEntity(
            id: inventory.Id,
            stock: inventory.Stock,
            isDeleted: inventory.IsDeleted,
            createdAtDatetime: inventory.CreatedAtDatetime,
            updatedAtDatetime: inventory.UpdatedAtDatetime,
            deletedAtDatetime: inventory.DeletedAtDatetime,
            createdAtTimezone: inventory.CreatedAtTimezone,
            updatedAtTimezone: inventory.UpdatedAtTimezone,
            deletedAtTimezone: inventory.DeletedAtTimezone,
            createdBy: inventory.CreatedBy,
            updatedBy: inventory.UpdatedBy,
            deletedBy: inventory.DeletedBy,
            productId: inventory.ProductId,
            variantId: inventory.VariantId,
            warehouseId: inventory.WarehouseId
        );
    }
}

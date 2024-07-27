using Bigpods.Monolith.Modules.Inventories.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Inventories.Domain.Common.Factories;

public static class InventoryEntityFactory
{
    public static InventoryEntity CreateOne(
        ICreateOneInventoryDto inventory,
        IInventoryModel? inventoryFoundById,
        IInventoryModel? inventoryFoundByWarehouseIdVariantId,
        IProductModel? productFoundById,
        IVariantModel? variantFoundById,
        IWarehouseModel? warehouseFoundById
    )
    {
        if (inventoryFoundById is not null)
        {
            throw new ConflictException("Inventory exist with this id");
        }

        if (inventoryFoundByWarehouseIdVariantId is not null)
        {
            throw new ConflictException("Inventory exist with this warehouse id and variant id");
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

        return new InventoryEntity(
            id: inventory.Id,
            stock: 0,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            createdAtTimezone: inventory.CreatedAtTimezone,
            createdBy: inventory.CreatedBy,
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

        return new InventoryEntity(
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
}

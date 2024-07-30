using Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Factories;

public static class InventoryInputEntityFactory
{
    public static InventoryInputEntity CreateOne(
        ICreateOneInventoryInputDto inventoryInput,
        IInventoryInputModel? inventoryInputFoundById,
        IInventoryModel? inventoryFoundById,
        IProductModel? productFoundById,
        IVariantModel? variantFoundById,
        IWarehouseModel? warehouseFoundById
    )
    {
        if (inventoryFoundById is null)
        {
            throw new NotFoundException("Inventory not found");
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

        if (inventoryInputFoundById is not null)
        {
            throw new ConflictException("Inventory input exist with this id");
        }

        return new InventoryInputEntity(
            id: inventoryInput.Id,
            stock: inventoryInput.Stock,
            comment: inventoryInput.Comment,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: inventoryInput.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: inventoryInput.CreatedBy,
            updatedBy: null,
            deletedBy: null,
            productId: inventoryInput.ProductId,
            variantId: inventoryInput.VariantId,
            warehouseId: inventoryInput.WarehouseId,
            inventoryId: inventoryInput.InventoryId
        );
    }
}

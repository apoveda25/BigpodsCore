using Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Factories;

public static class InventoryOutputEntityFactory
{
    public static InventoryOutputEntity CreateOne(
        ICreateOneInventoryOutputDto inventoryOutput,
        IInventoryOutputModel? inventoryOutputFoundById,
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

        if (inventoryOutputFoundById is not null)
        {
            throw new ConflictException("Inventory output exist with this id");
        }

        return new InventoryOutputEntity(
            id: inventoryOutput.Id,
            stock: inventoryOutput.Stock,
            comment: inventoryOutput.Comment,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            createdAtTimezone: inventoryOutput.CreatedAtTimezone,
            createdBy: inventoryOutput.CreatedBy,
            productId: inventoryOutput.ProductId,
            variantId: inventoryOutput.VariantId,
            warehouseId: inventoryOutput.WarehouseId,
            inventoryId: inventoryOutput.InventoryId
        );
    }
}

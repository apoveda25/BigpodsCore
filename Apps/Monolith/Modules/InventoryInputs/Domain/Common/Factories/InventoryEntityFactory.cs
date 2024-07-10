using Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Factories;

public sealed class InventoryEntityFactory
{
    public static InventoryEntity BuildOne(IInventoryModel? inventoryInput)
    {
        if (inventoryInput is null)
        {
            throw new NotFoundException("Inventory does not exist with this id");
        }

        if (inventoryInput.IsDeleted)
        {
            throw new ConflictException("Inventory is deleted");
        }

        return new InventoryEntity(
            id: inventoryInput.Id,
            stock: inventoryInput.Stock,
            isDeleted: inventoryInput.IsDeleted,
            createdAtDatetime: inventoryInput.CreatedAtDatetime,
            updatedAtDatetime: inventoryInput.UpdatedAtDatetime,
            deletedAtDatetime: inventoryInput.DeletedAtDatetime,
            createdAtTimezone: inventoryInput.CreatedAtTimezone,
            updatedAtTimezone: inventoryInput.UpdatedAtTimezone,
            deletedAtTimezone: inventoryInput.DeletedAtTimezone,
            createdBy: inventoryInput.CreatedBy,
            updatedBy: inventoryInput.UpdatedBy,
            deletedBy: inventoryInput.DeletedBy,
            productId: inventoryInput.ProductId,
            variantId: inventoryInput.VariantId,
            warehouseId: inventoryInput.WarehouseId
        );
    }
}

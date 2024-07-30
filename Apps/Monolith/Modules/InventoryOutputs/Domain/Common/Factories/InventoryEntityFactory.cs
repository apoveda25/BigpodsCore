using Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Entities;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.Common.Factories;

public static class InventoryEntityFactory
{
    public static InventoryEntity BuildOne(IInventoryModel? inventoryOutput)
    {
        if (inventoryOutput is null)
        {
            throw new NotFoundException("Inventory does not exist with this id");
        }

        if (inventoryOutput.IsDeleted)
        {
            throw new ConflictException("Inventory is deleted");
        }

        return new InventoryEntity(
            id: inventoryOutput.Id,
            stock: inventoryOutput.Stock,
            isDeleted: inventoryOutput.IsDeleted,
            createdAtDatetime: inventoryOutput.CreatedAtDatetime,
            updatedAtDatetime: inventoryOutput.UpdatedAtDatetime,
            deletedAtDatetime: inventoryOutput.DeletedAtDatetime,
            createdAtTimezone: inventoryOutput.CreatedAtTimezone,
            updatedAtTimezone: inventoryOutput.UpdatedAtTimezone,
            deletedAtTimezone: inventoryOutput.DeletedAtTimezone,
            createdBy: inventoryOutput.CreatedBy,
            updatedBy: inventoryOutput.UpdatedBy,
            deletedBy: inventoryOutput.DeletedBy,
            productId: inventoryOutput.ProductId,
            variantId: inventoryOutput.VariantId,
            warehouseId: inventoryOutput.WarehouseId
        );
    }
}

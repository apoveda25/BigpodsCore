using Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.Common.Factories;

public static class WarehouseAggregateRootFactory
{
    public static WarehouseAggregateRoot BuildOne(IWarehouseModel? warehouse)
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
}

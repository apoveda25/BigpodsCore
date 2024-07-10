using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Warehouses.Domain.Common.Aggregates;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.Common.Factories;

public sealed class WarehouseAggregateRootFactory
{
    public static WarehouseAggregateRoot CreateOne(
        ICreateOneWarehouseDto warehouse,
        IWarehouseModel? warehouseFoundById,
        IWarehouseModel? warehouseFoundByName
    )
    {
        if (warehouseFoundById is not null)
        {
            throw new ConflictException("Warehouse already exist with this id");
        }

        if (warehouseFoundByName is not null)
        {
            throw new ConflictException("Warehouse already exist with this name");
        }

        return new WarehouseAggregateRoot(
            id: warehouse.Id,
            name: warehouse.Name,
            description: warehouse.Description,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            createdAtTimezone: warehouse.CreatedAtTimezone,
            createdBy: warehouse.CreatedBy
        );
    }

    public static WarehouseAggregateRoot UpdateOne(
        IUpdateOneWarehouseDto warehouse,
        IWarehouseModel? warehouseFoundById,
        IWarehouseModel? warehouseFoundByName
    )
    {
        if (warehouseFoundById is null)
        {
            throw new NotFoundException("Warehouse not found with this id");
        }

        if (warehouseFoundById.IsDeleted == true)
        {
            throw new NotFoundException("Warehouse is deleted");
        }

        if (warehouseFoundById.Id != warehouse.Id)
        {
            throw new ConflictException("Warehouse id does not match");
        }

        if (warehouseFoundByName is not null && warehouseFoundByName.Id != warehouse.Id)
        {
            throw new ConflictException("Warehouse already exist with this name");
        }

        return new WarehouseAggregateRoot(
            id: warehouseFoundById.Id,
            name: warehouse?.Name ?? warehouseFoundById.Name,
            description: warehouse?.Description ?? warehouseFoundById.Description,
            isDeleted: warehouseFoundById.IsDeleted,
            createdAtDatetime: warehouseFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: warehouseFoundById.DeletedAtDatetime,
            createdAtTimezone: warehouseFoundById.CreatedAtTimezone,
            updatedAtTimezone: warehouse?.UpdatedAtTimezone,
            deletedAtTimezone: warehouseFoundById.DeletedAtTimezone,
            createdBy: warehouseFoundById.CreatedBy,
            updatedBy: warehouse?.UpdatedBy,
            deletedBy: warehouseFoundById.DeletedBy
        );
    }

    public static WarehouseAggregateRoot DeleteOne(
        IDeleteOneWarehouseDto warehouse,
        IWarehouseModel? warehouseFoundById,
        IInventoryModel[] inventoriesFoundByWarehouseId,
        IInventoryInputModel[] inventoryInputsFoundByWarehouseId,
        IInventoryOutputModel[] inventoryOutputsFoundByWarehouseId
    )
    {
        if (warehouseFoundById is null)
        {
            throw new NotFoundException("Warehouse not found with this id");
        }

        if (warehouseFoundById.IsDeleted == true)
        {
            throw new NotFoundException("Warehouse not found with this id");
        }

        if (inventoriesFoundByWarehouseId.Length != 0)
        {
            throw new ConflictException("Inventories exist with this warehouse id");
        }

        if (inventoryInputsFoundByWarehouseId.Length != 0)
        {
            throw new ConflictException("Inventory inputs exist with this warehouse id");
        }

        if (inventoryOutputsFoundByWarehouseId.Length != 0)
        {
            throw new ConflictException("Inventory outputs exist with this warehouse id");
        }

        return new WarehouseAggregateRoot(
            id: warehouseFoundById.Id,
            name: warehouseFoundById.Name,
            description: warehouseFoundById.Description,
            isDeleted: true,
            createdAtDatetime: warehouseFoundById.CreatedAtDatetime,
            updatedAtDatetime: warehouseFoundById.UpdatedAtDatetime,
            deletedAtDatetime: DateTime.Now,
            createdAtTimezone: warehouseFoundById.CreatedAtTimezone,
            updatedAtTimezone: warehouseFoundById.UpdatedAtTimezone,
            deletedAtTimezone: warehouse.DeletedAtTimezone,
            createdBy: warehouseFoundById.CreatedBy,
            updatedBy: warehouseFoundById.UpdatedBy,
            deletedBy: warehouse.DeletedBy
        );
    }
}

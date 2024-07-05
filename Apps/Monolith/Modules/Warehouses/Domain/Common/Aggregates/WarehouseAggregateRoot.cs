using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.Common.Aggregates;

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
    }

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

        if (warehouseFoundByName is not null && warehouseFoundByName.IsDeleted == false)
        {
            throw new ConflictException("Warehouse already exist with this name");
        }

        return new WarehouseAggregateRoot(
            id: warehouse.Id,
            name: warehouse.Name,
            description: warehouse.Description,
            isDeleted: false,
            createdAtDatetime: DateTime.Now,
            updatedAtDatetime: null,
            deletedAtDatetime: null,
            createdAtTimezone: warehouse.CreatedAtTimezone,
            updatedAtTimezone: null,
            deletedAtTimezone: null,
            createdBy: warehouse.CreatedBy,
            updatedBy: null,
            deletedBy: null
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

        if (warehouseFoundByName is not null && warehouseFoundByName.IsDeleted == false && warehouseFoundByName.Id != warehouse.Id)
        {
            throw new ConflictException("Warehouse already exist with this name");
        }

        return new WarehouseAggregateRoot(
            id: warehouseFoundById.Id,
            name: warehouse?.Name ?? warehouseFoundById.Name,
            description: warehouse?.Description ?? warehouseFoundById.Description,
            isDeleted: false,
            createdAtDatetime: warehouseFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: null,
            createdAtTimezone: warehouseFoundById.CreatedAtTimezone,
            updatedAtTimezone: warehouse?.UpdatedAtTimezone,
            deletedAtTimezone: null,
            createdBy: warehouseFoundById.CreatedBy,
            updatedBy: warehouse?.UpdatedBy,
            deletedBy: null
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

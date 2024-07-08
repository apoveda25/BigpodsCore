using Bigpods.Monolith.Modules.Shared.Domain.Exceptions;
using Bigpods.Monolith.Modules.Shared.Domain.ValueObjects;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Services;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Dtos;
using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Services;

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
        ICreateOneWarehouseServiceResponse data
    )
    {
        if (data.WarehouseFoundById is not null)
        {
            throw new ConflictException("Warehouse already exist with this id");
        }

        if (data.WarehouseFoundByName is not null)
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
        IUpdateOneWarehouseServiceResponse data
    )
    {
        if (data.WarehouseFoundById is null)
        {
            throw new NotFoundException("Warehouse not found with this id");
        }

        if (data.WarehouseFoundById.IsDeleted == true)
        {
            throw new NotFoundException("Warehouse is deleted");
        }

        if (data.WarehouseFoundById.Id != warehouse.Id)
        {
            throw new ConflictException("Warehouse id does not match");
        }

        if (data.WarehouseFoundByName is not null && data.WarehouseFoundByName.Id != warehouse.Id)
        {
            throw new ConflictException("Warehouse already exist with this name");
        }

        return new WarehouseAggregateRoot(
            id: data.WarehouseFoundById.Id,
            name: warehouse?.Name ?? data.WarehouseFoundById.Name,
            description: warehouse?.Description ?? data.WarehouseFoundById.Description,
            isDeleted: data.WarehouseFoundById.IsDeleted,
            createdAtDatetime: data.WarehouseFoundById.CreatedAtDatetime,
            updatedAtDatetime: DateTime.Now,
            deletedAtDatetime: data.WarehouseFoundById.DeletedAtDatetime,
            createdAtTimezone: data.WarehouseFoundById.CreatedAtTimezone,
            updatedAtTimezone: warehouse?.UpdatedAtTimezone,
            deletedAtTimezone: data.WarehouseFoundById.DeletedAtTimezone,
            createdBy: data.WarehouseFoundById.CreatedBy,
            updatedBy: warehouse?.UpdatedBy,
            deletedBy: data.WarehouseFoundById.DeletedBy
        );
    }

    public static WarehouseAggregateRoot DeleteOne(
        IDeleteOneWarehouseDto warehouse,
        IDeleteOneWarehouseServiceResponse data
    )
    {
        if (data.WarehouseFoundById is null)
        {
            throw new NotFoundException("Warehouse not found with this id");
        }

        if (data.WarehouseFoundById.IsDeleted == true)
        {
            throw new NotFoundException("Warehouse not found with this id");
        }

        if (data.InventoriesFoundByWarehouseId.Length != 0)
        {
            throw new ConflictException("Inventories exist with this warehouse id");
        }

        if (data.InventoryInputsFoundByWarehouseId.Length != 0)
        {
            throw new ConflictException("Inventory inputs exist with this warehouse id");
        }

        if (data.InventoryOutputsFoundByWarehouseId.Length != 0)
        {
            throw new ConflictException("Inventory outputs exist with this warehouse id");
        }

        return new WarehouseAggregateRoot(
            id: data.WarehouseFoundById.Id,
            name: data.WarehouseFoundById.Name,
            description: data.WarehouseFoundById.Description,
            isDeleted: true,
            createdAtDatetime: data.WarehouseFoundById.CreatedAtDatetime,
            updatedAtDatetime: data.WarehouseFoundById.UpdatedAtDatetime,
            deletedAtDatetime: DateTime.Now,
            createdAtTimezone: data.WarehouseFoundById.CreatedAtTimezone,
            updatedAtTimezone: data.WarehouseFoundById.UpdatedAtTimezone,
            deletedAtTimezone: warehouse.DeletedAtTimezone,
            createdBy: data.WarehouseFoundById.CreatedBy,
            updatedBy: data.WarehouseFoundById.UpdatedBy,
            deletedBy: warehouse.DeletedBy
        );
    }
}

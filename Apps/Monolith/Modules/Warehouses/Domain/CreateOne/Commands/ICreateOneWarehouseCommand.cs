using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Commands;

public interface ICreateOneWarehouseCommand
{
    ICreateOneWarehouseDto WarehouseDto { get; init; }
}

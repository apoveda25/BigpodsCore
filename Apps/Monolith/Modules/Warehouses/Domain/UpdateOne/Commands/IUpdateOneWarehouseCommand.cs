using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Commands;

public interface IUpdateOneWarehouseCommand
{
    IUpdateOneWarehouseDto WarehouseDto { get; init; }
}

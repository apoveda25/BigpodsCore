using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Commands;

public interface IDeleteOneWarehouseCommand
{
    IDeleteOneWarehouseDto WarehouseDto { get; init; }
}

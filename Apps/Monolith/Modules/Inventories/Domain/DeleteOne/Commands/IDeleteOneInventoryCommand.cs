using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Commands;

public interface IDeleteOneInventoryCommand
{
    IDeleteOneInventoryDto InventoryDto { get; init; }
}

using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Commands;

public interface ICreateOneInventoryCommand
{
    ICreateOneInventoryDto InventoryDto { get; init; }
}

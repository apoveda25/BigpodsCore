using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Commands;

public interface ICreateOneInventoryInputCommand
{
    ICreateOneInventoryInputDto InventoryInputDto { get; init; }
}

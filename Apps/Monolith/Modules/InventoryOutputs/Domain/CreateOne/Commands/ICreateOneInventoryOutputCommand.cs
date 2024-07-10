using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Commands;

public interface ICreateOneInventoryOutputCommand
{
    ICreateOneInventoryOutputDto InventoryOutputDto { get; init; }
}

using Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Application.CreateOne.Dtos;

public sealed record CreateOneInventoryOutputDto(
    Guid Id,
    int Stock,
    string Comment,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid ProductId,
    Guid VariantId,
    Guid WarehouseId,
    Guid InventoryId
) : ICreateOneInventoryOutputDto;

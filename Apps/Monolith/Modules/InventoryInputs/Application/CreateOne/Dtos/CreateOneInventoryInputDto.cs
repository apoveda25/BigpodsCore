using Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.InventoryInputs.Application.CreateOne.Dtos;

public sealed record CreateOneInventoryInputDto(
    Guid Id,
    int Stock,
    string Comment,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid ProductId,
    Guid VariantId,
    Guid WarehouseId,
    Guid InventoryId
) : ICreateOneInventoryInputDto;

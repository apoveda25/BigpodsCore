using Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Inventories.Application.CreateOne.Dtos;

public sealed record CreateOneInventoryDto(
    Guid Id,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid ProductId,
    Guid VariantId,
    Guid WarehouseId
) : ICreateOneInventoryDto;

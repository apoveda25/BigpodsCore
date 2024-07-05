using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Dtos;

public sealed record UpdateOneWarehouseDto(
    Guid Id,
    string? Name,
    string? Description,
    string UpdatedAtTimezone,
    Guid UpdatedBy
) : IUpdateOneWarehouseDto;

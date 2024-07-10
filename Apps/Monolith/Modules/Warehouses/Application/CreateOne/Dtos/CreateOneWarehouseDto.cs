using Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Warehouses.Application.CreateOne.Dtos;

public sealed record CreateOneWarehouseDto(
    Guid Id,
    string Name,
    string Description,
    string CreatedAtTimezone,
    Guid CreatedBy
) : ICreateOneWarehouseDto;

using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Dtos;

public sealed record DeleteOneWarehouseDto(
    Guid Id,
    string DeletedAtTimezone,
    Guid DeletedBy
) : IDeleteOneWarehouseDto;

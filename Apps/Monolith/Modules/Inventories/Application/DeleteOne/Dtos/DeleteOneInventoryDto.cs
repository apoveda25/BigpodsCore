using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Dtos;

public sealed record DeleteOneInventoryDto(Guid Id, string DeletedAtTimezone, Guid DeletedBy)
    : IDeleteOneInventoryDto;

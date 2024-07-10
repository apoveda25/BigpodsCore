using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Attributes.Application.DeleteOne.Dtos;

public sealed record DeleteOneAttributeDto(Guid Id, string DeletedAtTimezone, Guid DeletedBy)
    : IDeleteOneAttributeDto;

using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Dtos;

public sealed record UpdateOneAttributeTypeDto(
    Guid Id,
    string? Name,
    string? Description,
    string? ValuePattern,
    string? MeasuringUnitPattern,
    string UpdatedAtTimezone,
    Guid UpdatedBy
) : IUpdateOneAttributeTypeDto;

using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Dtos;

public sealed record CreateOneAttributeTypeDto(
    Guid Id,
    string Name,
    string Description,
    string ValuePattern,
    string MeasuringUnitPattern,
    string CreatedAtTimezone,
    Guid CreatedBy
) : ICreateOneAttributeTypeDto;

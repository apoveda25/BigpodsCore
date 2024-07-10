using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Dtos;

public sealed record CreateOneAttributeDto(
    Guid Id,
    string Value,
    string MeasuringUnit,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid AttributeTypeId
) : ICreateOneAttributeDto;

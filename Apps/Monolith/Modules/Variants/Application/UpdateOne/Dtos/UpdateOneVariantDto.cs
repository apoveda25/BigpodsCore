using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Dtos;

public sealed record UpdateOneVariantDto(
    Guid Id,
    string? Name,
    float? Price,
    float? Cost,
    string UpdatedAtTimezone,
    Guid UpdatedBy
) : IUpdateOneVariantDto;

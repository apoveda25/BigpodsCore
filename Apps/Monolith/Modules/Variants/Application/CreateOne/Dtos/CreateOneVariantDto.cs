using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Application.CreateOne.Dtos;

public sealed record CreateOneVariantDto(
    Guid Id,
    string Name,
    decimal Price,
    decimal Cost,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid ProductId
) : ICreateOneVariantDto;

public sealed record CreateOneVariantOnAttributeDto(
    Guid Id,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid AttributeId,
    Guid VariantId
) : ICreateOneVariantOnAttributeDto;

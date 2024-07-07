using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Products.Application.CreateOne.Dtos;

public record CreateOneProductDto(
    Guid Id,
    string Name,
    string Description,
    string Brand,
    string Model,
    string CreatedAtTimezone,
    Guid CreatedBy
) : ICreateOneProductDto;

public record CreateOneVariantDto(
    Guid Id,
    string Name,
    float Price,
    float Cost,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid ProductId
) : ICreateOneVariantDto;

public record CreateOneVariantOnAttributeDto(
    Guid Id,
    string CreatedAtTimezone,
    Guid CreatedBy,
    Guid VariantId,
    Guid AttributeId
) : ICreateOneVariantOnAttributeDto;

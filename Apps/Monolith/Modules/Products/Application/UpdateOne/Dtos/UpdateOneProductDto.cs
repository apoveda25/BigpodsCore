using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Products.Application.UpdateOne.Dtos;

public record UpdateOneProductDto(
    Guid Id,
    string? Name,
    string? Description,
    string? Brand,
    string? Model,
    string UpdatedAtTimezone,
    Guid UpdatedBy
) : IUpdateOneProductDto;

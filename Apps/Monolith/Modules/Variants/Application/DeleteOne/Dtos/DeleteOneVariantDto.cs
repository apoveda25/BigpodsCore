using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Dtos;

public sealed record DeleteOneVariantDto(Guid Id, string DeletedAtTimezone, Guid DeletedBy)
    : IDeleteOneVariantDto;

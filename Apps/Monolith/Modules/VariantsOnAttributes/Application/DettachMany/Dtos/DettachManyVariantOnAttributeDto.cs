using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Dtos;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Dtos;

public sealed record DettachManyVariantOnAttributeDto(Guid ProductId)
    : IDettachManyVariantOnAttributeDto;

public sealed record DeleteOneVariantOnAttributeDto(
    Guid Id,
    string DeletedAtTimezone,
    Guid DeletedBy
) : IDeleteOneVariantOnAttributeDto;

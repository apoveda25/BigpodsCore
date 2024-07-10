using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Dtos;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Commands;

public interface IDettachManyVariantOnAttributeCommand
{
    IDettachManyVariantOnAttributeDto ProductDto { get; init; }
    IDeleteOneVariantOnAttributeDto[] VariantOnAttributeDtos { get; init; }
}

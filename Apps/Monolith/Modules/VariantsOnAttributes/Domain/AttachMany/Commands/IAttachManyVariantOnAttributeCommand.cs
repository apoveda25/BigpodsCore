using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Dtos;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Commands;

public interface IAttachManyVariantOnAttributeCommand
{
    IAttachManyVariantOnAttributeDto ProductDto { get; init; }
    ICreateOneVariantOnAttributeDto[] VariantOnAttributeDtos { get; init; }
}

using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Commands;

public interface ICreateOneVariantCommand
{
    ICreateOneVariantDto VariantDto { get; init; }
    ICreateOneVariantOnAttributeDto[] VariantOnAttributeDtos { get; init; }
}

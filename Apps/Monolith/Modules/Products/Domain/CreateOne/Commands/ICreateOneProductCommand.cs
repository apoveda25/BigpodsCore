using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Products.Domain.CreateOne.Commands;

public interface ICreateOneProductCommand
{
    ICreateOneProductDto ProductDto { get; init; }
    ICreateOneVariantDto[] VariantDtos { get; init; }
    ICreateOneVariantOnAttributeDto[] VariantOnAttributeDtos { get; init; }
}

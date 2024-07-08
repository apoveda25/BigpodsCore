using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Products.Domain.CreateOne.Commands;

public interface ICreateOneProductCommand
{
    public ICreateOneProductDto ProductDto { get; init; }
    public ICreateOneVariantDto[] VariantDtos { get; init; }
    public ICreateOneVariantOnAttributeDto[] VariantOnAttributeDtos { get; init; }
}

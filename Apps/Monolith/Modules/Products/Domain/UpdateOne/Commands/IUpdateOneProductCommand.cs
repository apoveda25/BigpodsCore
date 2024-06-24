using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Commands;

public interface IUpdateOneProductCommand
{
    IUpdateOneProductDto ProductDto { get; init; }
}

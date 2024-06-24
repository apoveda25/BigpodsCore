using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Commands;

public interface IDeleteOneVariantCommand
{
    IDeleteOneVariantDto VariantDto { get; init; }
}

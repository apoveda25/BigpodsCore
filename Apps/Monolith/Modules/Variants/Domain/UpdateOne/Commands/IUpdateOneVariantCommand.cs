using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Commands;

public interface IUpdateOneVariantCommand
{
    IUpdateOneVariantDto VariantDto { get; init; }
}

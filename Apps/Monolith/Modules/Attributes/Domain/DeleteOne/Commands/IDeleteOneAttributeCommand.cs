using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Dtos;

namespace Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Commands;

public interface IDeleteOneAttributeCommand
{
    IDeleteOneAttributeDto AttributeDto { get; init; }
}

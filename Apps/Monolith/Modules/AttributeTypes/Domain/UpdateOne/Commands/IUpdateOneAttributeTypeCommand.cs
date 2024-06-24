using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Dtos;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Commands;

public interface IUpdateOneAttributeTypeCommand
{
    IUpdateOneAttributeTypeDto AttributeTypeDto { get; init; }
}

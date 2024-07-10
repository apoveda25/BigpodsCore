using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Commands;

public interface ICreateOneAttributeTypeCommand
{
    ICreateOneAttributeTypeDto AttributeTypeDto { get; init; }
}

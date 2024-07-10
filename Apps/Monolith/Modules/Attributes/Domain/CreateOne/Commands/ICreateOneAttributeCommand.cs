using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Dtos;

namespace Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Commands;

public interface ICreateOneAttributeCommand
{
    ICreateOneAttributeDto AttributeDto { get; init; }
}

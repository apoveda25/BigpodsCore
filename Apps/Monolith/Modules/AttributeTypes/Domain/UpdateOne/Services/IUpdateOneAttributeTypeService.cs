using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Services;

public interface IUpdateOneAttributeTypeService
{
    public Task<IUpdateOneAttributeTypeServiceResponse> ExecuteAsync(
        IUpdateOneAttributeTypeCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IUpdateOneAttributeTypeServiceResponse
{
    public IAttributeTypeModel? AttributeTypeFoundById { get; init; }
    public IAttributeTypeModel? AttributeTypeFoundByName { get; init; }
}
using Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Services;

public interface IDeleteOneAttributeService
{
    public Task<IDeleteOneAttributeServiceResponse> ExecuteAsync(
        IDeleteOneAttributeCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IDeleteOneAttributeServiceResponse
{
    public IAttributeModel? AttributeFoundById { get; init; }
    public IAttributeTypeModel? AttributeTypeFoundById { get; init; }
}
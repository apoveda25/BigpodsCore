using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Services;

public interface ICreateOneAttributeTypeService
{
    public Task<ICreateOneAttributeTypeServiceResponse> ExecuteAsync(
        ICreateOneAttributeTypeCommand command,
        CancellationToken cancellationToken
    );
}

public interface ICreateOneAttributeTypeServiceResponse
{
    public IAttributeTypeModel? AttributeTypeFoundById { get; init; }
    public IAttributeTypeModel? AttributeTypeFoundByName { get; init; }
}
using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Services;

public interface ICreateOneAttributeService
{
    public Task<ICreateOneAttributeServiceResponse> ExecuteAsync(
        ICreateOneAttributeCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface ICreateOneAttributeServiceResponse
{
    public IAttributeModel? AttributeFoundById { get; init; }
    public IAttributeModel? AttributeFoundByValueMeasuringUnitAttributeTypeId { get; init; }
    public IAttributeTypeModel? AttributeTypeFoundById { get; init; }
}
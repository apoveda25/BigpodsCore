using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Commands;

namespace Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Services;

public interface IDeleteOneVariantService
{
    public Task<IDeleteOneVariantServiceResponse> ExecuteAsync(
        IDeleteOneVariantCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IDeleteOneVariantServiceResponse
{
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel? VariantFoundById { get; init; }
    public IVariantOnAttributeModel[] VariantsOnAttributesFoundByVariantId { get; init; }
}
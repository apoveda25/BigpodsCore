using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Commands;

namespace Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Services;

public interface IUpdateOneVariantService
{
    public Task<IUpdateOneVariantServiceResponse> ExecuteAsync(
        IUpdateOneVariantCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IUpdateOneVariantServiceResponse
{
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel? VariantFoundById { get; init; }
}
using Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Services;

public interface IUpdateOneProductService
{
    public Task<IUpdateOneProductServiceResponse> ExecuteAsync(
        IUpdateOneProductCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IUpdateOneProductServiceResponse
{
    public IProductModel? ProductFoundById { get; init; }
}

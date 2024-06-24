using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Commands;
using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Products.Domain.CreateOne.Services;

public interface ICreateOneProductService
{
    public Task<ICreateOneProductServiceResponse> ExecuteAsync(
        ICreateOneProductCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface ICreateOneProductServiceResponse
{
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel[] VariantsFoundById { get; init; }
    public IVariantOnAttributeModel[] VariantsOnAttributesFoundById { get; init; }
    public IAttributeModel[] AttributesFoundById { get; init; }
}

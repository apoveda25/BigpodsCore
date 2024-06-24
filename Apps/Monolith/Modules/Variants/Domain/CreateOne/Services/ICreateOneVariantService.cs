using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Commands;

namespace Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Services;

public interface ICreateOneVariantService
{
    public Task<ICreateOneVariantServiceResponse> ExecuteAsync(
        ICreateOneVariantCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface ICreateOneVariantServiceResponse
{
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel? VariantFoundById { get; init; }
    public IVariantModel[] VariantsFoundByProductId { get; init; }
    public IVariantOnAttributeModel[] VariantsOnAttributesFoundById { get; init; }
    public IVariantOnAttributeModel[] VariantsOnAttributesFoundByVariantId { get; init; }
    public IAttributeModel[] AttributesFoundById { get; init; }
}

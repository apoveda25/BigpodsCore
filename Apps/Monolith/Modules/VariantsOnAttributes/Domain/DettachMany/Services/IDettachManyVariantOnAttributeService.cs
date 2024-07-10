using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Commands;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Services;

public interface IDettachManyVariantOnAttributeService
{
    public Task<IDettachManyVariantOnAttributeServiceResponse> ExecuteAsync(
        IDettachManyVariantOnAttributeCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IDettachManyVariantOnAttributeServiceResponse
{
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel[] VariantsFoundById { get; init; }
    public IVariantOnAttributeModel[] VariantsOnAttributesFoundById { get; init; }
}

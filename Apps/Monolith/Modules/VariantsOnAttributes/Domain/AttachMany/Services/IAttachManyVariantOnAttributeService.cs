using Bigpods.Monolith.Modules.Shared.Domain.Models;
using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Commands;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Services;

public interface IAttachManyVariantOnAttributeService
{
    public Task<IAttachManyVariantOnAttributeServiceResponse> ExecuteAsync(
        IAttachManyVariantOnAttributeCommand command,
        CancellationToken cancellationToken = default
    );
}

public interface IAttachManyVariantOnAttributeServiceResponse
{
    public IProductModel? ProductFoundById { get; init; }
    public IVariantModel[] VariantsFoundById { get; init; }
    public IVariantOnAttributeModel[] VariantsOnAttributesFoundById { get; init; }
    public IVariantOnAttributeModel[] VariantsOnAttributesFoundByVariantIdAttributeId { get; init; }
    public IAttributeModel[] AttributesFoundById { get; init; }
}

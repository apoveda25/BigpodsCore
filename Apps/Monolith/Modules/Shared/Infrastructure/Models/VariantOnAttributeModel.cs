using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "VariantOnAttributeType")]
public sealed class VariantOnAttributeModel : BaseModel, IVariantOnAttributeModel
{
    public Guid VariantId { get; set; } = default!;
    public Guid AttributeId { get; set; } = default!;

    public VariantModel Variant { get; set; } = default!;
    public AttributeModel Attribute { get; set; } = default!;
}

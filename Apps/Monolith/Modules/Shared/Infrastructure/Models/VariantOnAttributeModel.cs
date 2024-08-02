using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "VariantOnAttribute")]
public sealed class VariantOnAttributeModel : BaseModel, IVariantOnAttributeModel
{
    public Guid VariantId { get; set; } = Guid.NewGuid();
    public Guid AttributeId { get; set; } = Guid.NewGuid();

    public VariantModel Variant { get; set; } = default!;
    public AttributeModel Attribute { get; set; } = default!;
}

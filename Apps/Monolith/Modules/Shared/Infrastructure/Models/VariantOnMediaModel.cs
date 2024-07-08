using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "VariantOnMediaType")]
public sealed class VariantOnMediaModel : BaseModel, IVariantOnMediaModel
{
    public Guid MediaId { get; set; } = default!;
    public Guid VariantId { get; set; } = default!;

    public MediaModel Media { get; set; } = default!;
    public VariantModel Variant { get; set; } = default!;
}

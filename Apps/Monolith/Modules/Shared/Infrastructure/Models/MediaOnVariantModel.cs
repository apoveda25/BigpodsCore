using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "MediaOnVariantType")]
public sealed class MediaOnVariantModel : BaseModel, IMediaOnVariantModel
{
    public Guid MediaId { get; set; } = default!;
    public Guid VariantId { get; set; } = default!;
}

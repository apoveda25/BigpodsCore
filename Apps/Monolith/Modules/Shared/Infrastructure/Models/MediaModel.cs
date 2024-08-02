using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "Media")]
public sealed class MediaModel : BaseModel, IMediaModel
{
    public string Path { get; set; } = default!;
    public string Url { get; set; } = default!;
    public int Order { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string Extension { get; set; } = default!;

    public ICollection<VariantOnMediaModel> VariantsOnMedias { get; set; } = default!;
}

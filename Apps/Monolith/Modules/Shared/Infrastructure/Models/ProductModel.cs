using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "Product")]
public sealed class ProductModel : BaseModel, IProductModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public bool IsCompleted { get; set; } = default!;
    public bool IsPublished { get; set; } = default!;

    public ICollection<VariantModel> Variants { get; set; } = default!;
}

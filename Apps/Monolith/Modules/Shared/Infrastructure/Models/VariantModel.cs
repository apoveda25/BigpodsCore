using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "VariantType")]
public sealed class VariantModel : BaseModel, IVariantModel
{
    public string Name { get; set; } = default!;
    public string Sku { get; set; } = default!;
    public float Price { get; set; } = default!;
    public float Cost { get; set; } = default!;
    public int Stock { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public ProductModel Product { get; set; } = default!;
    public ICollection<VariantOnAttributeModel> VariantsOnAttributes { get; set; } = default!;
}

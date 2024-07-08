using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "InventoryOutputType")]
public sealed class InventoryOutputModel : BaseModel, IInventoryOutputModel
{
    public int Stock { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public Guid VariantId { get; set; } = default!;
    public Guid WarehouseId { get; set; } = default!;
    public Guid InventoryId { get; set; } = default!;

    public ProductModel Product { get; set; } = default!;
    public VariantModel Variant { get; set; } = default!;
    public WarehouseModel Warehouse { get; set; } = default!;
    public InventoryModel Inventory { get; set; } = default!;
}

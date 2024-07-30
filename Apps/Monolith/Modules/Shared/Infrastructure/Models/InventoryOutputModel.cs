using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "InventoryOutputType")]
public sealed class InventoryOutputModel : BaseModel, IInventoryOutputModel
{
    public int Stock { get; set; } = default!;
    public string Comment { get; set; } = default!;
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public Guid VariantId { get; set; } = Guid.NewGuid();
    public Guid WarehouseId { get; set; } = Guid.NewGuid();
    public Guid InventoryId { get; set; } = Guid.NewGuid();

    public ProductModel Product { get; set; } = default!;
    public VariantModel Variant { get; set; } = default!;
    public WarehouseModel Warehouse { get; set; } = default!;
    public InventoryModel Inventory { get; set; } = default!;
}

using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "Inventory")]
public sealed class InventoryModel : BaseModel, IInventoryModel
{
    public int Stock { get; set; } = default!;
    public Guid ProductId { get; set; } = Guid.NewGuid();
    public Guid VariantId { get; set; } = Guid.NewGuid();
    public Guid WarehouseId { get; set; } = Guid.NewGuid();

    public ProductModel Product { get; set; } = default!;
    public VariantModel Variant { get; set; } = default!;
    public WarehouseModel Warehouse { get; set; } = default!;
    public ICollection<InventoryInputModel> InventoryInputs { get; set; } = default!;
    public ICollection<InventoryOutputModel> InventoryOutputs { get; set; } = default!;
}

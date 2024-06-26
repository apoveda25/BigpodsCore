using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "InventoryType")]
public sealed class InventoryModel : BaseModel, IInventoryModel
{
    public int Stock { get; set; } = default!;
    public Guid VariantId { get; set; } = default!;
    public Guid WarehouseId { get; set; } = default!;
    public VariantModel Variant { get; set; } = default!;
    public WarehouseModel Warehouse { get; set; } = default!;
    public ICollection<InventoryInputModel> InventoryInputs { get; set; } = default!;
    public ICollection<InventoryOutputModel> InventoryOutputs { get; set; } = default!;
}

using Bigpods.Monolith.Modules.Shared.Domain.Models;

namespace Bigpods.Monolith.Modules.Shared.Infrastructure.Models;

[GraphQLName(name: "Warehouse")]
public sealed class WarehouseModel : BaseModel, IWarehouseModel
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public ICollection<InventoryModel> Inventories { get; set; } = default!;
    public ICollection<InventoryInputModel> InventoryInputs { get; set; } = default!;
    public ICollection<InventoryOutputModel> InventoryOutputs { get; set; } = default!;
}

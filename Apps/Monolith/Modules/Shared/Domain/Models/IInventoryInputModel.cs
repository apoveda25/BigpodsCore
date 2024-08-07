namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IInventoryInputModel : IBaseModel
{
    public int Stock { get; set; }
    public string Comment { get; set; }
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid InventoryId { get; set; }
}

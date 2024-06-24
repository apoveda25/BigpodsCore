namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IInventoryModel : IBaseModel
{
    public int Stock { get; set; }
    public Guid VariantId { get; set; }
    public Guid WarehouseId { get; set; }
}

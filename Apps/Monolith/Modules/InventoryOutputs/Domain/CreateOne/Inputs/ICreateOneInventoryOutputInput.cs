using NodaTime;

namespace Bigpods.Monolith.Modules.InventoryOutputs.Domain.CreateOne.Inputs;

public interface ICreateOneInventoryOutputInput
{
    public Optional<Guid> Id { get; set; }
    public int Stock { get; set; }
    public string Comment { get; set; }
    public DateTimeZone CreatedAtTimezone { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid InventoryId { get; set; }
}

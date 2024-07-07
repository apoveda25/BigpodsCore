using NodaTime;

namespace Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Inputs;

public interface ICreateOneInventoryInput
{
    public Optional<Guid> Id { get; set; }
    public DateTimeZone CreatedAtTimezone { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }
    public Guid WarehouseId { get; set; }
}

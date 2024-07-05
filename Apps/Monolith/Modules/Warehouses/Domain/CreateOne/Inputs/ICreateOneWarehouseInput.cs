using NodaTime;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Inputs;

public interface ICreateOneWarehouseInput
{
    public Optional<Guid> Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeZone CreatedAtTimezone { get; set; }
    public Guid CreatedBy { get; set; }
}

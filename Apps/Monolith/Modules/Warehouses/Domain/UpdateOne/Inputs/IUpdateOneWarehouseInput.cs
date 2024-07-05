using NodaTime;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Inputs;

public interface IUpdateOneWarehouseInput
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTimeZone UpdatedAtTimezone { get; set; }
    public Guid UpdatedBy { get; set; }
}

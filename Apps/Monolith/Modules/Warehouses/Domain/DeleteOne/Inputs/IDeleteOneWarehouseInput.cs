using NodaTime;

namespace Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Inputs;

public interface IDeleteOneWarehouseInput
{
    public Guid Id { get; set; }
    public DateTimeZone DeletedAtTimezone { get; set; }
    public Guid DeletedBy { get; set; }
}

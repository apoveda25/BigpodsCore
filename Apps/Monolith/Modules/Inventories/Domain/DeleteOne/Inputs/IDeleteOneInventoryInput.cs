using NodaTime;

namespace Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Inputs;

public interface IDeleteOneInventoryInput
{
    public Guid Id { get; set; }
    public DateTimeZone DeletedAtTimezone { get; set; }
    public Guid DeletedBy { get; set; }
}

using Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Inventories.Application.DeleteOne.Inputs;

public sealed record DeleteOneInventoryInput : IDeleteOneInventoryInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public DateTimeZone DeletedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid DeletedBy { get; set; } = Guid.Empty;
}

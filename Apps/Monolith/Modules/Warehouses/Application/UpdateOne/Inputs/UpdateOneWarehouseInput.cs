using Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Warehouses.Application.UpdateOne.Inputs;

public sealed record UpdateOneWarehouseInput : IUpdateOneWarehouseInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTimeZone UpdatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid UpdatedBy { get; set; } = Guid.Empty;
}

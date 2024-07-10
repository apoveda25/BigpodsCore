using Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Warehouses.Application.DeleteOne.Inputs;

[GraphQLName(name: "WarehouseDeleteOneWarehouseInput")]
public sealed record DeleteOneWarehouseInput : IDeleteOneWarehouseInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public DateTimeZone DeletedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid DeletedBy { get; set; } = Guid.Empty;
}

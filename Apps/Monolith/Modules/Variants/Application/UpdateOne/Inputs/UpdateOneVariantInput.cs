using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Inputs;

public sealed record UpdateOneVariantInput : IUpdateOneVariantInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public decimal? Cost { get; set; }

    public DateTimeZone UpdatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid UpdatedBy { get; set; } = Guid.Empty;
}

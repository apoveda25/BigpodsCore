using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Inputs;

using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Inputs;

[GraphQLName(name: "VariantUpdateOneVariantInput")]
public sealed record UpdateOneVariantInput : IUpdateOneVariantInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public string? Name { get; set; }

    public float? Price { get; set; }

    public float? Cost { get; set; }

    public DateTimeZone UpdatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid UpdatedBy { get; set; } = Guid.Empty;
}

using Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Inputs;

using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Application.UpdateOne.Inputs;

[GraphQLName(name: "VariantUpdateOneVariantInput")]
public sealed record UpdateOneVariantInput : IUpdateOneVariantInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public string? Name { get; set; } = string.Empty;

    public float? Price { get; set; } = 0;

    public float? Cost { get; set; } = 0;

    public DateTimeZone UpdatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid UpdatedBy { get; set; } = Guid.Empty;
}

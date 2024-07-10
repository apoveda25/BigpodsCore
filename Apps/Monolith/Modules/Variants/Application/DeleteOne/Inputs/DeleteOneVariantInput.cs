using Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Application.DeleteOne.Inputs;

[GraphQLName(name: "VariantDeleteOneVariantInput")]
public sealed record DeleteOneVariantInput : IDeleteOneVariantInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public DateTimeZone DeletedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid DeletedBy { get; set; } = Guid.Empty;
}

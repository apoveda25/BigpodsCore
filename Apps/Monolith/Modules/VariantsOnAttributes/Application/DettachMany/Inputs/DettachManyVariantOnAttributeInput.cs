using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.DettachMany.Inputs;

public sealed record DettachManyVariantOnAttributeInput
    : IDettachManyVariantOnAttributeInput<DeleteOneVariantOnAttributeInput>
{
    public Guid ProductId { get; set; } = Guid.Empty;

    public DeleteOneVariantOnAttributeInput[] VariantsOnAttributes { get; set; } = [];
}

[GraphQLName(name: "VariantOnAttributeDeleteOneVariantOnAttributeInput")]
public sealed record DeleteOneVariantOnAttributeInput : IDeleteOneVariantOnAttributeInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public DateTimeZone DeletedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid DeletedBy { get; set; } = Guid.Empty;
}

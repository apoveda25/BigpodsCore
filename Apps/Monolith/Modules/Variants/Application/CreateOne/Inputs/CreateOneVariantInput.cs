using Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Inputs;

using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Application.CreateOne.Inputs;

[GraphQLName(name: "VariantCreateOneVariantInput")]
public sealed record CreateOneVariantInput : ICreateOneVariantInput<CreateOneVariantOnAttributeInput>
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id { get => _id; set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value; }

    public string Name { get; set; } = string.Empty;

    public float Price { get; set; } = 0;

    public float Cost { get; set; } = 0;

    public int Stock { get; set; } = 0;

    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;

    public Guid ProductId { get; set; } = Guid.Empty;

    public CreateOneVariantOnAttributeInput[] VariantsOnAttributes { get; set; } = [];
}

[GraphQLName(name: "VariantCreateOneVariantOnAttributeInput")]
public sealed record CreateOneVariantOnAttributeInput : ICreateOneVariantOnAttributeInput
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id { get => _id; set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value; }

    [GraphQLIgnore]
    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;

    [GraphQLIgnore]
    public Guid VariantId { get; set; } = Guid.Empty;

    public Guid AttributeId { get; set; } = Guid.Empty;
}

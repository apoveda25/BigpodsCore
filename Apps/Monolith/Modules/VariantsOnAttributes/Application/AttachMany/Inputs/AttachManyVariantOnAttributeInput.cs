using Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.AttachMany.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Application.AttachMany.Inputs;

[GraphQLName(name: "VariantOnAttributeAttachManyVariantOnAttributeInput")]
public sealed record AttachManyVariantOnAttributeInput
    : IAttachManyVariantOnAttributeInput<CreateOneVariantOnAttributeInput>
{
    public Guid ProductId { get; set; } = Guid.Empty;

    public CreateOneVariantOnAttributeInput[] VariantsOnAttributes { get; set; } = [];
}

[GraphQLName(name: "VariantOnAttributeCreateOneVariantOnAttributeInput")]
public sealed record CreateOneVariantOnAttributeInput : ICreateOneVariantOnAttributeInput
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id
    {
        get => _id;
        set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value;
    }

    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;

    public Guid VariantId { get; set; } = Guid.Empty;

    public Guid AttributeId { get; set; } = Guid.Empty;
}

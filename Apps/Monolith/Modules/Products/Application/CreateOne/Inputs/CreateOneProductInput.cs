using Bigpods.Monolith.Modules.Products.Domain.CreateOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Products.Application.CreateOne.Inputs;

public sealed record CreateOneProductInput : ICreateOneProductInput<CreateOneVariantInput>
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id
    {
        get => _id;
        set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value;
    }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;

    public CreateOneVariantInput[] Variants { get; set; } = [];
}

[GraphQLName(name: "ProductCreateOneVariantInput")]
public record CreateOneVariantInput : ICreateOneVariantInput<CreateOneVariantOnAttributeInput>
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id
    {
        get => _id;
        set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value;
    }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; } = 0;

    public decimal Cost { get; set; } = 0;

    [GraphQLIgnore]
    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;

    [GraphQLIgnore]
    public Guid ProductId { get; set; } = Guid.Empty;

    public CreateOneVariantOnAttributeInput[] VariantsOnAttributes { get; set; } = [];
}

[GraphQLName(name: "ProductCreateOneVariantOnAttributeInput")]
public record CreateOneVariantOnAttributeInput : ICreateOneVariantOnAttributeInput
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id
    {
        get => _id;
        set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value;
    }

    [GraphQLIgnore]
    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;

    [GraphQLIgnore]
    public Guid VariantId { get; set; } = Guid.Empty;

    public Guid AttributeId { get; set; } = Guid.Empty;
}

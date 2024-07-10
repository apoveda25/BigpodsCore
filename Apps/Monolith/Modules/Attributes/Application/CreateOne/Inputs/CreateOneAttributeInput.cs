using Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Inputs;
using NodaTime;

namespace Bigpods.Monolith.Modules.Attributes.Application.CreateOne.Inputs;

[GraphQLName(name: "AttributeCreateOneAttributeInput")]
public sealed record CreateOneAttributeInput : ICreateOneAttributeInput
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id
    {
        get => _id;
        set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value;
    }

    public string Value { get; set; } = string.Empty;

    public string MeasuringUnit { get; set; } = string.Empty;

    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;

    public Guid AttributeTypeId { get; set; } = Guid.Empty;
}

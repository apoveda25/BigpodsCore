using Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Inputs;

using NodaTime;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.CreateOne.Inputs;

[GraphQLName(name: "AttributeTypeCreateOneAttributeTypeInput")]
public sealed record CreateOneAttributeTypeInput : ICreateOneAttributeTypeInput
{
    private Guid _id;

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Optional<Guid> Id { get => _id; set => _id = Guid.Empty.CompareTo(value.Value) == 0 ? Guid.NewGuid() : value; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ValuePattern { get; set; } = string.Empty;

    public string MeasuringUnitPattern { get; set; } = string.Empty;

    public DateTimeZone CreatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid CreatedBy { get; set; } = Guid.Empty;
}

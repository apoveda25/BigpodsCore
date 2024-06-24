
using Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Inputs;

using NodaTime;

namespace Bigpods.Monolith.Modules.AttributeTypes.Application.UpdateOne.Inputs;

[GraphQLName(name: "AttributeTypeUpdateOneAttributeTypeInput")]
public sealed record UpdateOneAttributeTypeInput : IUpdateOneAttributeTypeInput
{
    public Guid Id { get; set; } = Guid.Empty;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ValuePattern { get; set; }

    public string? MeasuringUnitPattern { get; set; }

    public DateTimeZone UpdatedAtTimezone { get; set; } = DateTimeZone.Utc;

    [GraphQLIgnore]
    public Guid UpdatedBy { get; set; } = Guid.Empty;
}

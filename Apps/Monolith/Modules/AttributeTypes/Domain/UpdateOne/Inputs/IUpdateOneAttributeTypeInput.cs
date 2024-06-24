using NodaTime;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.UpdateOne.Inputs;

public interface IUpdateOneAttributeTypeInput
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ValuePattern { get; set; }
    public string? MeasuringUnitPattern { get; set; }
    public DateTimeZone UpdatedAtTimezone { get; set; }
    public Guid UpdatedBy { get; set; }
}

using NodaTime;

namespace Bigpods.Monolith.Modules.AttributeTypes.Domain.CreateOne.Inputs;

public interface ICreateOneAttributeTypeInput
{
    public Optional<Guid> Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ValuePattern { get; set; }
    public string MeasuringUnitPattern { get; set; }
    public DateTimeZone CreatedAtTimezone { get; set; }
    public Guid CreatedBy { get; set; }
}

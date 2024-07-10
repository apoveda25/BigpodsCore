using NodaTime;

namespace Bigpods.Monolith.Modules.Attributes.Domain.CreateOne.Inputs;

public interface ICreateOneAttributeInput
{
    public Optional<Guid> Id { get; set; }

    public string Value { get; set; }

    public string MeasuringUnit { get; set; }

    public DateTimeZone CreatedAtTimezone { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid AttributeTypeId { get; set; }
}

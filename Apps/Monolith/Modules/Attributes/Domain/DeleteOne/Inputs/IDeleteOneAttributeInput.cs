using NodaTime;

namespace Bigpods.Monolith.Modules.Attributes.Domain.DeleteOne.Inputs;

public interface IDeleteOneAttributeInput
{
    public Guid Id { get; set; }

    public DateTimeZone DeletedAtTimezone { get; set; }

    public Guid DeletedBy { get; set; }
}
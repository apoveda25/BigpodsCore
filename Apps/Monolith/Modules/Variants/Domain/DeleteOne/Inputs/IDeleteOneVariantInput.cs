using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Inputs;

public interface IDeleteOneVariantInput
{
    public Guid Id { get; set; }
    public DateTimeZone DeletedAtTimezone { get; set; }
    public Guid DeletedBy { get; set; }
}

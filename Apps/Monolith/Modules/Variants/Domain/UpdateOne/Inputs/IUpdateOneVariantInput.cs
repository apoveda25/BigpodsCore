using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Inputs;

public interface IUpdateOneVariantInput
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public float? Price { get; set; }
    public float? Cost { get; set; }
    public DateTimeZone UpdatedAtTimezone { get; set; }
    public Guid UpdatedBy { get; set; }
}

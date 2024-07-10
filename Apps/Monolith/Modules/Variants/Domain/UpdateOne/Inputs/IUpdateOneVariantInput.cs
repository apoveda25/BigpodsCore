using NodaTime;

namespace Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Inputs;

public interface IUpdateOneVariantInput
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public decimal? Cost { get; set; }
    public DateTimeZone UpdatedAtTimezone { get; set; }
    public Guid UpdatedBy { get; set; }
}

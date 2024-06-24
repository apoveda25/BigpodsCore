using NodaTime;

namespace Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Inputs;

public interface IUpdateOneProductInput
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTimeZone UpdatedAtTimezone { get; set; }
    public Guid UpdatedBy { get; set; }
}

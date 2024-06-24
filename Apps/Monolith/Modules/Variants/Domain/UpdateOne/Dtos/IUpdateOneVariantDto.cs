namespace Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Dtos;

public interface IUpdateOneVariantDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public float? Price { get; init; }
    public float? Cost { get; init; }
    public string UpdatedAtTimezone { get; init; }
    public Guid UpdatedBy { get; init; }
}

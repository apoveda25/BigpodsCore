namespace Bigpods.Monolith.Modules.Variants.Domain.UpdateOne.Dtos;

public interface IUpdateOneVariantDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public decimal? Cost { get; init; }
    public string UpdatedAtTimezone { get; init; }
    public Guid UpdatedBy { get; init; }
}

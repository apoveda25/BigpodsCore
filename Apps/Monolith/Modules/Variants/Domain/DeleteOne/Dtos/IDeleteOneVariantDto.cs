namespace Bigpods.Monolith.Modules.Variants.Domain.DeleteOne.Dtos;

public interface IDeleteOneVariantDto
{
    public Guid Id { get; init; }
    public string DeletedAtTimezone { get; init; }
    public Guid DeletedBy { get; init; }
}

namespace Bigpods.Monolith.Modules.VariantsOnAttributes.Domain.DettachMany.Dtos;

public interface IDettachManyVariantOnAttributeDto
{
    public Guid ProductId { get; init; }
}

public interface IDeleteOneVariantOnAttributeDto
{
    public Guid Id { get; init; }
    public string DeletedAtTimezone { get; init; }
    public Guid DeletedBy { get; init; }
}

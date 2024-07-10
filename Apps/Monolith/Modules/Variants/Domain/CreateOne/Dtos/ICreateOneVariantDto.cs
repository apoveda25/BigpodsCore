namespace Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;

public interface ICreateOneVariantDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public decimal Cost { get; init; }
    public string CreatedAtTimezone { get; init; }
    public Guid CreatedBy { get; init; }
    public Guid ProductId { get; init; }
}

public interface ICreateOneVariantOnAttributeDto
{
    public Guid Id { get; init; }
    public string CreatedAtTimezone { get; init; }
    public Guid CreatedBy { get; init; }
    public Guid AttributeId { get; init; }
    public Guid VariantId { get; init; }
}

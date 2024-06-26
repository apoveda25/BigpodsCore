namespace Bigpods.Monolith.Modules.Variants.Domain.CreateOne.Dtos;

public interface ICreateOneVariantDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public float Price { get; init; }
    public float Cost { get; init; }
    public int Stock { get; init; }
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
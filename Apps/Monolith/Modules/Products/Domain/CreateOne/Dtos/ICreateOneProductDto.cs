namespace Bigpods.Monolith.Modules.Products.Domain.CreateOne.Dtos;

public interface ICreateOneProductDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Brand { get; init; }
    public string Model { get; init; }
    public string CreatedAtTimezone { get; init; }
    public Guid CreatedBy { get; init; }
}

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
    public Guid VariantId { get; init; }
    public Guid AttributeId { get; init; }
}

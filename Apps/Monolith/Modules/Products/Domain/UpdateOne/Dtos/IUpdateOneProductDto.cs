namespace Bigpods.Monolith.Modules.Products.Domain.UpdateOne.Dtos;

public interface IUpdateOneProductDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Brand { get; init; }
    public string? Model { get; init; }
    public string UpdatedAtTimezone { get; init; }
    public Guid UpdatedBy { get; init; }
}

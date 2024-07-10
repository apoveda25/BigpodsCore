namespace Bigpods.Monolith.Modules.Warehouses.Domain.UpdateOne.Dtos;

public interface IUpdateOneWarehouseDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string UpdatedAtTimezone { get; init; }
    public Guid UpdatedBy { get; init; }
}

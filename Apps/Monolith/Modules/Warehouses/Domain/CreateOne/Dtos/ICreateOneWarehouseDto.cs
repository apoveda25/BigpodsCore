namespace Bigpods.Monolith.Modules.Warehouses.Domain.CreateOne.Dtos;

public interface ICreateOneWarehouseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string CreatedAtTimezone { get; init; }
    public Guid CreatedBy { get; init; }
}

namespace Bigpods.Monolith.Modules.Warehouses.Domain.DeleteOne.Dtos;

public interface IDeleteOneWarehouseDto
{
    public Guid Id { get; init; }
    public string DeletedAtTimezone { get; init; }
    public Guid DeletedBy { get; init; }
}

namespace Bigpods.Monolith.Modules.Inventories.Domain.DeleteOne.Dtos;

public interface IDeleteOneInventoryDto
{
    public Guid Id { get; init; }
    public string DeletedAtTimezone { get; init; }
    public Guid DeletedBy { get; init; }
}

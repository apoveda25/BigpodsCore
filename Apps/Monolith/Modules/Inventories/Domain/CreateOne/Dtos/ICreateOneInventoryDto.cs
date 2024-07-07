namespace Bigpods.Monolith.Modules.Inventories.Domain.CreateOne.Dtos;

public interface ICreateOneInventoryDto
{
    public Guid Id { get; init; }
    public string CreatedAtTimezone { get; init; }
    public Guid CreatedBy { get; init; }
    public Guid ProductId { get; init; }
    public Guid VariantId { get; init; }
    public Guid WarehouseId { get; init; }
}

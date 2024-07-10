namespace Bigpods.Monolith.Modules.InventoryInputs.Domain.CreateOne.Dtos;

public interface ICreateOneInventoryInputDto
{
    public Guid Id { get; init; }
    public int Stock { get; init; }
    public string Comment { get; init; }
    public string CreatedAtTimezone { get; init; }
    public Guid CreatedBy { get; init; }
    public Guid ProductId { get; init; }
    public Guid VariantId { get; init; }
    public Guid WarehouseId { get; init; }
    public Guid InventoryId { get; init; }
}

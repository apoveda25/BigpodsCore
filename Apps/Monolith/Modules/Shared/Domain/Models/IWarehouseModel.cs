namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IWarehouseModel : IBaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
}

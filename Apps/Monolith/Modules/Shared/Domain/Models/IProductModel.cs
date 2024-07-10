namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IProductModel : IBaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsPublished { get; set; }
}

namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IVariantModel : IBaseModel
{
    public string Name { get; set; }
    public string Sku { get; set; }
    public float Price { get; set; }
    public float Cost { get; set; }
    public int Stock { get; set; }
    public Guid ProductId { get; set; }
}

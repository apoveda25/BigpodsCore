namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IVariantModel : IBaseModel
{
    public string Name { get; set; }
    public string Sku { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public Guid ProductId { get; set; }
}

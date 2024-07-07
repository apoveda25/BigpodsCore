namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IVariantOnMediaModel : IBaseModel
{
    public Guid MediaId { get; set; }
    public Guid VariantId { get; set; }
}

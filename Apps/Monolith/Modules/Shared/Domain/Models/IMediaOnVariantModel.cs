namespace Bigpods.Monolith.Modules.Shared.Domain.Models;

public interface IMediaOnVariantModel : IBaseModel
{
    public Guid MediaId { get; set; }
    public Guid VariantId { get; set; }
}
